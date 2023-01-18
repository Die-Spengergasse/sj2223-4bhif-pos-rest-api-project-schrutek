using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services.Customers.Queries
{
    public static class CustomerQueryExtensions
    {
    //    public static ICustomerRequestHandler<GetFilteredCustomerQuery, IQueryable<Customer>> UseContainsFilter(
    //        this ICustomerRequestHandler<GetFilteredCustomerQuery, 
    //        IQueryable<Customer>> service, 
    //        string filter)
    //    {
    //        Expression<Func<Customer, bool>>? filterExprssion = null;
    //        if (!string.IsNullOrEmpty(filter))
    //        {
    //            filterExprssion = s => s.LastName.Contains(filter);
    //        }
    //        service.Filter = filterExprssion;
    //        return service;
    //    }
    }
}
