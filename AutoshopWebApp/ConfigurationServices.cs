using AutoshopWebApp.Authorization.Handlers;
using AutoshopWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddCRUDServices(this IServiceCollection services)
        {
            services
                .AddScoped<ICarService, CarService>()
                .AddScoped<ISparePartService, SparePartService>();

            return services;
        }

        public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, WorkerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TransactionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CarAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, BuyerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SellerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StateAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ExpertiseAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SparePartAuthorizationHandler>();

            return services;
        }
    }
}
