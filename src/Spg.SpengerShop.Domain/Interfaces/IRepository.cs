using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> SingleOrDefaultAsync(Guid id, CancellationToken cancellationToken);

        Task<TEntity> AddAsync(Customer customer);
    }
}
