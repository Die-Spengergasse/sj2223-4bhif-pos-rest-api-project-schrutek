using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly IReadOnlyRepositoryBase<Customer> _repository;

        public CustomerRepository(IReadOnlyRepositoryBase<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            // TODO: EFCore-Create-Stuff
            return customer;
        }

        public async Task<Customer> SingleOrDefaultAsync(Guid id, CancellationToken cancellationToken)
        {
            return null;
            //return await _repository.GetSingleAsync(c => c.Guid == id) 
            //    ?? throw new KeyNotFoundException("Customer not Found!");
        }
    }
}
