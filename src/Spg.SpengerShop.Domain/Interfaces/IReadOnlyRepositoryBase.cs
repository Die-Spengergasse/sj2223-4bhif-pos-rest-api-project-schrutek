using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IReadOnlyRepositoryBase<TEntity> 
        where TEntity : class
    {
        TEntity? GetById<TKey>(TKey id);

        T? GetSingleOrDefaultByGuid<T>(Guid guid) where T : class, IFindableByGuid;

        public Task<IQueryable<TEntity>> GetQueryable(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
            string? includeNavigationProperty = null,
            int? skip = null,
            int? take = null);

        public Task<IQueryable<TEntity>> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);

        public Task<IQueryable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeNavigationProperty = "",
            int? skip = null,
            int? take = null);
    }
}
