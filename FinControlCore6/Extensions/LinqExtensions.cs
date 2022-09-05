using FinControlCore6.Models.AuxiliaryModels;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Linq.Expressions.Expression;

namespace FinControlCore6.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> query,
            string orderByMember,
            DataTableOrderDir direction)
        {
            var param = Expression.Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var queryable = direction == DataTableOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Expression.Lambda(body, param));

            return queryable;
        }


        public static IQueryable<T> WhereDynamic<T>(
                this IQueryable<T> sourceList, string query, string propertyName = "")
        {
            if (string.IsNullOrEmpty(query))
                return sourceList;

            Type elementType = typeof(T);

            PropertyInfo[] properties = elementType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => x.CanRead && x.CanWrite && (!x.GetGetMethod()?.IsVirtual ?? false)).ToArray();
            if (!properties.Any())
                return sourceList;
            
            ParameterExpression parameter = Parameter(elementType);

            IEnumerable<MemberExpression> propertiesExpressions = properties.Select(
                property => Property(parameter, property));

            IEnumerable<MethodInfo?> toStringMethods = properties.Select(
                property => property.PropertyType.GetMethod("ToString",Type.EmptyTypes));
            if (toStringMethods.Any(m => m == null))
                throw new Exception($"Can not find ToString method for property of type {toStringMethods.First(m => m == null)}");

            var tempExprs = propertiesExpressions.Zip(toStringMethods);

            IEnumerable<Expression> expressionsToString = tempExprs.Select<(MemberExpression,MethodInfo?),Expression>(
                pair => pair.Item1.Type == typeof(string)? pair.Item1: Call(pair.Item1, pair.Item2!));

            MethodInfo? containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            if (containsMethod == null)
                throw new Exception("Rigth overload of Contains method not found");

            IEnumerable<Expression> expressions = expressionsToString.Select(expression =>
            Call(expression, containsMethod, Constant(query)));


            Expression body = expressions.Aggregate((prev,current) => Or(prev, current));

            Expression<Func<T, bool>> lambda = Lambda<Func<T,bool>>(body, parameter);
            return sourceList.Where(lambda);
            //if (!string.IsNullOrEmpty(propertyName))
            //{
            //    var property = elementType.GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            //    if (property is null)
            //        throw new Exception($"Property with name \"{propertyName}\" not found");
            //    if (!property.CanRead)
            //        throw new Exception($"Can not read property with name \"{property.Name}\"");
            //    sourceList = sourceList.Where(c => property.GetValue(c).ToString().Contains(query, StringComparison.InvariantCultureIgnoreCase));
            //}
            //else
            //{
            //    //var properties = typeof(T).GetProperties().Where(x => x.CanRead && x.CanWrite && (!x.GetGetMethod()?.IsVirtual ?? false));

            //    //Expression
            //    sourceList = sourceList.Where(c => c != null /*&& EF.Functions.Like(c.ToString()!, $"%{query}%")*/);
            //    //properties..Any(checkPropertyValue(c, query))); ;
            //}
            //return sourceList;
        }

        static Func<PropertyInfo, bool> checkPropertyValue<T>(T c, string query)
        {
            bool pred(PropertyInfo p)
            {
                object? propValue = p.GetValue(c);
                if (propValue != null)
                {
                    bool searchRes = propValue.ToString()?.Contains(query, StringComparison.InvariantCultureIgnoreCase) ?? false;
                    return searchRes;
                }
                return false;
            }
            return pred;
        }
        //public static IQueryable<T> WhereByPropertyDynamic<T>(
        //        this IQueryable<T> sourceList, string query, string propertyName)
        //{
        //    var property = typeof(T).GetProperty(propertyName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        //    if (!property.CanRead || property is null)
        //        throw new Exception($"Can not read property with name \"{property.Name}\"");

        //    return sourceList;
        //}

        //private static bool CheckContains<T>(PropertyInfo property, T obj)
        //{

        //}
    }
}
