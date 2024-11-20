using MERENT_API.Middlewares;
using Application;
using FluentValidation.AspNetCore;
using Infrastructures;
using System.Diagnostics;
using Application.Interfaces;
using Application.Services;
using Application.Repositories;
using Infrastructures.Repositories;
using MERENT_API.Service;

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
            services.AddScoped<IPayOSService, PayOSService>();
            services.AddHttpContextAccessor();
            // infrastructure service
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClaimsService, ClaimServices>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            
            services.AddScoped<IComboRepository, ComboRepository>();

            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IPODetailRepository, PODetailRepository>();
            services.AddScoped<IComboOfProductRepository, ComboOfProductRepository>();

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

            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IComboOfProductService, ComboOfProductService>();

            services.AddHttpContextAccessor();










            return services;
        }

    }
}

