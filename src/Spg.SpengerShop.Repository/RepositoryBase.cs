using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    {
        private readonly SpengerShopContext _db;

        public RepositoryBase(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(TEntity newEntity)
        {
            // TODO: NULL-Handling
            if (newEntity is null)
            {
                throw new RepositoryCreateException($"{nameof(TEntity)} war NULL!");
            }

            // TODO: Exception Handling
            _db.Add(newEntity);
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryCreateException($"Create ist für {nameof(TEntity)} fehlgeschlagen!", ex);
            }
        }

        public void Delete<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Update<TKey>(TKey id, TEntity newEntity)
        {
            throw new NotImplementedException();
        }
    }
}
