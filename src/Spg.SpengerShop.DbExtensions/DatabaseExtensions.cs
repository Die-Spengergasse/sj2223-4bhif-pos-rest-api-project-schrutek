using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.SpengerShop.Infrastructure;

namespace Spg.SpengerShop.DbExtensions
{
    public static class DatabaseExtensions
    {
        public static void ConfigureSqLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SpengerShopContext>(options => 
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlite(connectionString);
                }
            });
        }

        public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SpengerShopContext>(options =>
            {
                if (!options.IsConfigured)
                {
                    options.UseSqlServer(connectionString);
                }
            });
        }

        //public static void GenerateDb(this IServiceCollection services)
        //{
        //}
    }
}