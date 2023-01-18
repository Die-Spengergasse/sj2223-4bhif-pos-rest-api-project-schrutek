using MediatR;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public GetCustomerByIdQuery(Guid guid)
        {
            Guid = guid;
        }

        public Guid Guid { get; set; }
    }
}
