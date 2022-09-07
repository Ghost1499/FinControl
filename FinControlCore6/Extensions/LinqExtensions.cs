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


        public static IQueryable<T> WhereDynamic<T>(
                this IQueryable<T> sourceList, Dictionary<string, string> columnsQueries)
        {
            if (!columnsQueries.Any())
                return sourceList;

            Type elementType = typeof(T);

            IEnumerable<PropertyInfo> properties = Utils.Utils.GetProperties(elementType);
            if (!properties.Any())
                return sourceList;

            ParameterExpression parameter = Parameter(elementType);

            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) }) ?? throw new Exception("Rigth overload of Contains method not found");

            List<Expression> columnsSearchExpressions = new List<Expression>();
            foreach (var (columnName, propertyQuery) in columnsQueries)
            {
                IEnumerable<PropertyInfo> columnProperties = columnName == "" ? properties : properties.Where(p => p.Name.ToUpperInvariant() == columnName.ToUpperInvariant());
                if (!columnProperties.Any())
                    throw new Exception($"Property with name {columnName} not found");

                IEnumerable<MemberExpression> columnPropertiesExpressions = columnProperties.Select(
                property => Property(parameter, property));

                IEnumerable<MethodInfo?> toStringMethods = columnProperties.Select(
                delegate (PropertyInfo property)
                {
                    if (property.PropertyType == typeof(string))
                        return null;
                    MethodInfo? toStringMethod = property.PropertyType.GetMethod("ToString", Type.EmptyTypes);
                    if (toStringMethod == null)
                        throw new Exception($"Can not find ToString method for property of type {property.PropertyType}");
                    return toStringMethod!;
                });

                var tempExprs = columnPropertiesExpressions.Zip(toStringMethods);

                IEnumerable<Expression> expressionsToString = tempExprs.Select<(MemberExpression, MethodInfo?), Expression>(
                    ((MemberExpression property, MethodInfo? toStringMethod) exprMembers) => exprMembers.toStringMethod != null ? Call(exprMembers.property, exprMembers.toStringMethod) : exprMembers.property);

                IEnumerable<Expression> expressions = expressionsToString.Select(expression => Call(expression, containsMethod, Constant(propertyQuery)));

                Expression columnBody = expressions.Aggregate((prev, current) => OrElse(prev, current));
                columnsSearchExpressions.Add(columnBody);
            }

            Expression body = columnsSearchExpressions.Aggregate((prev, current) => AndAlso(prev, current));

            Expression<Func<T, bool>> lambda = Lambda<Func<T, bool>>(body, parameter);
            return sourceList.Where(lambda);
        }
    }
}
