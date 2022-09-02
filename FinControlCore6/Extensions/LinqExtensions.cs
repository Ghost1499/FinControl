using FinControlCore6.Models.AuxiliaryModels;
using System.Linq.Expressions;
using System;
using System.Linq;

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


    }
}
