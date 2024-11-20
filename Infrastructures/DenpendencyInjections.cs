using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Domain.Entites;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructures
{
    public static class DenpendencyInjections
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(databaseConnection));

            //add repositories injection
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IComboRepository, ComboRepository>();

            services.AddScoped<IProductOrderRepository, ProductOrderRepository>();
            services.AddScoped<IPODetailRepository, PODetailRepository>();

            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IComboOfProductRepository, ComboOfProductRepository>();


            // add generic repository
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            services.AddScoped<IGenericRepository<Combo>, GenericRepository<Combo>>();
            services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddScoped<IGenericRepository<Service>, GenericRepository<Service>>();
            services.AddScoped<IGenericRepository<Store>, GenericRepository<Store>>();

            services.AddScoped<IGenericRepository<ProductOrder>, GenericRepository<ProductOrder>>();
            services.AddScoped<IGenericRepository<ProductOrderDetails>, GenericRepository<ProductOrderDetails>>();

            services.AddScoped<IGenericRepository<Wallet>, GenericRepository<Wallet>>();
            services.AddScoped<IGenericRepository<Transaction>, GenericRepository<Transaction>>();
            services.AddScoped<IGenericRepository<ComboOfProduct>, GenericRepository<ComboOfProduct>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            return services;
        }
    }
}
