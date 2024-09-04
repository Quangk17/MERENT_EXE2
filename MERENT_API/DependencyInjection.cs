using MERENT_API.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Diagnostics;
using FluentValidation.AspNetCore;

namespace MERENT_API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddMemoryCache();
            // infrastructure service
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // controller API  service
            
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            




            return services;
        }

    }
}
}
