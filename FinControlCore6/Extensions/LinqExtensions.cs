using FinControlCore6.Models.AuxiliaryModels;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FinControlCore6.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Linq.Expressions.Expression;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FinControlCore6.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            DataTableOrderDir direction)
        {
            var param = Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, PropertyOrField);

            var queryable = direction == DataTableOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Lambda(body, param));

            return queryable;
        }


        // Названия колонок в запросе должны совпадать с названиями свойств модели
        public static IQueryable<T> WhereDynamic<T>(
                this IQueryable<T> sourceList, string globalQuery, Dictionary<string, string> columnsQueries)
        {
            if (!columnsQueries.Any() && string.IsNullOrEmpty(globalQuery))
                return sourceList;
            Type elementType = typeof(T);

            IEnumerable<PropertyInfo> properties = ReflectionUtils.GetProperties(elementType);
            if (!properties.Any())
                return sourceList;

            ParameterExpression parameter = Parameter(elementType);

            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) }) ?? throw new Exception("Rigth overload of Contains method was not found");

            //PropertyToColumnComparer propertyToColumnComparer = new PropertyToColumnComparer();

            IEnumerable<PropertyInfo> queryedProperties = globalQuery != "" ? properties :
            // columns left joined to properties
            //(from cq in columnsQueries.Keys
            // join p in properties on cq.ToUpperInvariant() equals p.Name.ToUpperInvariant() into columnOnProp
            // from subProp in columnOnProp.DefaultIfEmpty()
            // select new
            // {
            //     columnQuery = cq.ToUpperInvariant(),
            //     property = subProp ?? null
            // })
            // .Select(q => q.property ?? throw new Exception($"Property for column {q.columnQuery} was not found"));
            columnsQueries.Keys.GroupJoin(properties, cq => cq, p => p.Name, (cq, props) => new { cq, props } /*,propertyToColumnComparer*/)
            .SelectMany(propOnColQ => propOnColQ.props.DefaultIfEmpty(), (propOnColQ, p) => new { columnQuery = propOnColQ.cq, property = p ?? null })
            .Select(q => q.property ?? throw new Exception($"Property for column {q.columnQuery} was not found"));

            Dictionary<Type, MethodInfo> propertiesTypesToStringMethods = queryedProperties.Select(p => p.PropertyType).Distinct().Except(new[] { typeof(string) }).ToDictionary(pType => pType,
                delegate (Type propertyType)
                {
                    MethodInfo? toStringMethod = propertyType.GetMethod("ToString", Type.EmptyTypes);
                    if (toStringMethod == null)
                        throw new Exception($"Can not find ToString method for property of type {propertyType}");
                    return toStringMethod!;
                });


            var propsToStringExprsDict = queryedProperties.ToDictionary(p => p,
                delegate (PropertyInfo property)
                {
                    var toStrMethod = propertiesTypesToStringMethods.GetValueOrDefault(property.PropertyType);
                    var propExpr = Property(parameter, property);
                    Expression toStrExpr = toStrMethod == null ? propExpr : Call(propExpr, toStrMethod);
                    return toStrExpr;
                });


            var globalQueryExpression = string.IsNullOrEmpty(globalQuery) ? null : queryedProperties.Select(delegate (PropertyInfo property)
            {
                var toStrExpr = propsToStringExprsDict[property];
                Expression columnExpr = Call(toStrExpr, containsMethod, Constant(globalQuery));
                return columnExpr;
            }).Aggregate((prev, current) => OrElse(prev, current));

            IEnumerable<Expression> singleColumnQueryExpressions = !columnsQueries.Any() ? new List<Expression>() : queryedProperties.Select(delegate (PropertyInfo property)
            {
                var toStrExpr = propsToStringExprsDict[property];
                var query = columnsQueries.GetValueOrDefault(property.Name);
                Expression? queryExpr = null;
                if (query != null)
                    queryExpr = Call(toStrExpr, containsMethod, Constant(query));
                return queryExpr;
            }).Where(expr => expr != null).Select(expr => expr!);

            var resultExpression = (globalQueryExpression == null ?singleColumnQueryExpressions : singleColumnQueryExpressions.Append(globalQueryExpression)).Aggregate((prev, current) => AndAlso(prev, current));


            Expression<Func<T, bool>> lambda = Lambda<Func<T, bool>>(resultExpression, parameter);
            return sourceList.Where(lambda);
        }

    }
}
