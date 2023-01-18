using MediatR;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public CreateCustomerCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Customer);
        }
    }
}
