using System.Linq.Expressions;

namespace AplicacaoEFCore.Pagination
{
    public static class QueriableExtensions
    {
        public static IQueryable<TEntity> GetPaginationQueriable<TEntity>
            (this IQueryable<TEntity> queriable, PaginationFilterRequest filter)
        {
            var props = typeof(TEntity).GetProperties();
            bool first = true;
            filter.FixNames();
            foreach (var item in filter.OrdersBy)
            {
                var prop = props?.FirstOrDefault(x => x.Name.Equals(item.PropertyName,
                    StringComparison.OrdinalIgnoreCase));
                if (prop == default)
                    throw new InvalidOperationException($"Couldn`t find property {item.PropertyName} " +
                        $"on {typeof(TEntity).Name}");

                ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
                MemberExpression lambProp = Expression.Property(parameter, prop.Name);
                dynamic selector = Expression.Lambda(lambProp, new ParameterExpression[] { parameter });

                if (item.SortType == SortType.Ascending)
                {
                    if (first)
                        queriable = Queryable.OrderBy(queriable, selector);
                    else if (queriable is IOrderedQueryable<TEntity> ordered)
                    {
                        queriable = Queryable.ThenBy(ordered, selector);
                    }
                }
                else if (item.SortType == SortType.Descending)
                {
                    if (first)
                        queriable = Queryable.OrderByDescending(queriable, selector);
                    else if (queriable is IOrderedQueryable<TEntity> ordered)
                        queriable = Queryable.ThenByDescending(ordered, selector);
                }
                first = false;
            }
            return queriable.Skip(filter.Skip).Take(filter.MaxRecordsPerPage);
        }
    }
}
