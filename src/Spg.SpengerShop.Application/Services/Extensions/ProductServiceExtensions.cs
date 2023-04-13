using Spg.SpengerShop.Domain.Interfaces;

namespace Spg.SpengerShop.Application.Services.Extensions
{
    public static class ProductServiceExtensions
    {
        public static IReadOnlyProductService UseNameContainsFilter(this IReadOnlyProductService service, string filter)
        {
            service.Products = service.Products.Where(p => p.Name.Contains(filter));
            return service;
        }

        public static IReadOnlyProductService UseFilterByExpiryDate(this IReadOnlyProductService service, DateTime from, DateTime to)
        {
            // TODO: Implementation
            return service;
        }

        public static IReadOnlyProductService UseFilterByStock(this IReadOnlyProductService service, int moreThan)
        {
            // TODO: Implementation
            return service;
        }

        // UseFilterBy...
        // ...

        public static IReadOnlyProductService UseSorting(this IReadOnlyProductService service, string columnName)
        {
            // TODO: sorting and toggle sort
            return service;
        }

        // Dont Do: UsePaging
    }
}
