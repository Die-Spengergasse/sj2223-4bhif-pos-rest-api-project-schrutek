using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository
{
    public class RepositoryBase<TKey, TEntity> : IRepositoryBase<TKey, TEntity>
    {
        private readonly SpengerShopContext _db;

        public RepositoryBase(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(TEntity newEntity)
        {
            _db.Add(newEntity);
            _db.SaveChanges();
        }

        public void Delete(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Update(TKey id, TEntity newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
