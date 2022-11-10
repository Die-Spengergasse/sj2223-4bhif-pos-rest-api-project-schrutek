using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Core
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        public static void ConfigureSqLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SpengerShopContext>(setup =>
            {
                if (!setup.IsConfigured)
                {
                    setup.UseSqlite(connectionString);
                }
            });
        }
    }
}
