using Currency.SQLServer.DAL;
using Currency.SQLServer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Currency.Service
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCurrencyServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<WebDbContextConnectionStrings>(options =>
            {
                options.ConnectionStrings.Add("Default", config.GetConnectionString("Default"));
            }).AddDbContext<WebDbContext>();
            services.AddScoped<CurrencyService>();
            services.AddScoped<CurrencyRepository>();
           
            return services;
        }
    }
}
