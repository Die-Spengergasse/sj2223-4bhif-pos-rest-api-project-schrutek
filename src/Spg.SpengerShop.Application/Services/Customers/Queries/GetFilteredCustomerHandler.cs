using MediatR;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Queries
{
    public class GetFilteredCustomerHandler : IRequestHandler<GetFilteredCustomerQuery, IQueryable<Customer>>
    {
        private readonly IReadOnlyRepositoryBase<Customer> _repository;

        public GetFilteredCustomerHandler(IReadOnlyRepositoryBase<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Customer>> Handle(GetFilteredCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request.Filter is not null)
            {
                return await _repository.Get(request.Filter);
            }
            return await _repository.GetAll();
        }
    }
}
