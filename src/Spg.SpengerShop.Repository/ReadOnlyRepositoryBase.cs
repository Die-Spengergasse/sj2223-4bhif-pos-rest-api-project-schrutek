using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository
{
    public class ReadOnlyRepositoryBase<TEntity> : IReadOnlyRepositoryBase<TEntity>
        where TEntity : class, new()
    {
        public SpengerShopContext Context { get; }

        public ReadOnlyRepositoryBase(SpengerShopContext context)
        {
            Context = context;
        }

        public async Task<IQueryable<TEntity>> GetQueryable(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
            string? includeNavigationProperty = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();

            if (filter != null)
            {
                result = result.Where(filter);
            }
            if (sortOrder != null)
            {
                result = sortOrder(result);
            }

            includeNavigationProperty = includeNavigationProperty ?? String.Empty;
            foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(item);
            }

            int count = result.Count();
            if (skip.HasValue)
            {
                result = result.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                result = result.Take(take.Value);
            }
            return result;
        }

        public async Task<IQueryable<TEntity>> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(
                null,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }

        public async Task<IQueryable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(
                filter,
                orderBy,
                includeNavigationProperty,
                skip,
                take
            );
        }

        //public async Task<TResponse> GetSingleAsync(
        //    Expression<Func<TResponse, bool>>? filter = null,
        //    string includeNavigationProperty = "")
        //{
        //    return await GetQueryable(
        //        filter,
        //        null,
        //        includeNavigationProperty: includeNavigationProperty
        //    );
        //}
    }
}
