using MediatR;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly IReadOnlyRepositoryBase<Customer> _repository;

        public GetCustomerByIdQueryHandler(IReadOnlyRepositoryBase<Customer> repository)
        {
            _repository = repository;
        }

        public Expression<Func<Product, bool>>? Filter { get; set; }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return null;
            //return await _repository.GetSingleAsync();
        }
    }
}
