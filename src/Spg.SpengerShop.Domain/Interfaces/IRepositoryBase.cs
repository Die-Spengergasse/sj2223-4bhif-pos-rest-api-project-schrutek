using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IRepositoryBase<TKey, TEntity>
    {
        void Create(TEntity newEntity);

        void Update(TKey id, TEntity newEntity);

        void Delete(TKey id);
    }
}
