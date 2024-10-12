using Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public IStoreRepository StoreRepository { get; }
        public IComboRepository ComboRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        
        public IPODetailRepository PODetailRepository { get; }
        public IProductOrderRepository ProductOrderRepository { get; }




        public Task<int> SaveChangeAsync();
    }
}
