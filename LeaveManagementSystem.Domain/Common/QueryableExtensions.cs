using System.Linq.Expressions;

namespace LeaveManagementSystem.Domain.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string sortBy)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortBy);
        var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

        return source.OrderBy(lambda);
    }
    public static IQueryable<T> OrderByDescendingDynamic<T>(this IQueryable<T> source, string sortBy)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortBy);
        var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

        return source.OrderByDescending(lambda);
    }
}