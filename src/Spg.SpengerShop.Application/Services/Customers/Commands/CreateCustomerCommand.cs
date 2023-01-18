using MediatR;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; } = default!;

        public CreateCustomerCommand(Customer customer)
        {
            Customer = customer;
        }
    }
}
