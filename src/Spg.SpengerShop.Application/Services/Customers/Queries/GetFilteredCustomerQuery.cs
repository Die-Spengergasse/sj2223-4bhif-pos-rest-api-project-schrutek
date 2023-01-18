using Azure.Core.Pipeline;
using MediatR;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Queries
{
    public class GetFilteredCustomerQuery : IRequest<IQueryable<Customer>>
    {
        public Expression<Func<Customer, bool>>? Filter { get; private set; } = default;

        public Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? Order { get; private set; }

        public GetFilteredCustomerQuery(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Filter = s => s.LastName.Contains(filter);
            }
        }

        public GetFilteredCustomerQuery(string filter, string orderby)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Filter = s => s.LastName.Contains(filter);
            }

            switch (orderby)
            {
                case "fname":
                    Order = (s) => s.OrderBy(s => s.FirstName);
                    break;
                case "fname_desc":
                    Order = s => s.OrderByDescending(s => s.FirstName);
                    break;
                case "lname":
                    Order = s => s.OrderBy(s => s.LastName);
                    break;
                case "lname_desc":
                    Order = s => s.OrderByDescending(s => s.LastName);
                    break;
                case "id":
                    Order = s => s.OrderBy(s => s.Id);
                    break;
                case "id_desc":
                    Order = s => s.OrderByDescending(s => s.Id);
                    break;
                default:
                    goto case "id";
            }
        }
    }
}
