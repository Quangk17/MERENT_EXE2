using MERENT_API.Middlewares;
using Application;
using FluentValidation.AspNetCore;
using Infrastructures;
using System.Diagnostics;
using Application.Interfaces;
using Application.Services;
using Application.Repositories;
using Infrastructures.Repositories;

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

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IComboRepository, ComboRepository>();

            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IPODetailRepository, PODetailRepository>();
            // controller API  service
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IComboService, ComboService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IPOService, ProductOrderService>();
            services.AddScoped<IPODetailService, PODetailService>();

            services.AddHttpContextAccessor();










            return services;
        }

    }
}

