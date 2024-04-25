using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CVBuilder.Repository.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> IncludeProperties<TEntity>(this IQueryable<TEntity> query,
        string includeProperties)
        where TEntity : class
    {
        if (!string.IsNullOrEmpty(includeProperties))
            foreach (var property in SplitProperties(includeProperties))
                query = query.Include(property);

        return query;
    }

    private static IEnumerable<string> SplitProperties(string includeProperties)
    {
        return includeProperties
            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }
}