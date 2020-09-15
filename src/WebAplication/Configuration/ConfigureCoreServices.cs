using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupermarketCheckout.Core.Interfaces;
using SupermarketCheckout.Core.Services;
using SupermarketCheckout.Infrastructure.Data;

namespace SupermarketCheckout.WebAplication.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ISkuService, SkuService>();
            services.AddScoped<ICheckoutService, CheckoutService>();

            return services;
        }
    }
}
