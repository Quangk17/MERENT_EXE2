
using Application;
using Application.Repositories;


namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IAccountRepository _accountRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IComboRepository _comboRepository;

        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IPODetailRepository _pODetailRepository;

        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;


        //

        public UnitOfWork(AppDbContext dbContext, 
            IAccountRepository accountRepository, 
            IComboRepository comboRepository, 
            IServiceRepository serviceRepository,
            IStoreRepository storeRepository,
            IRoleRepository roleRepository, 
            IProductRepository productRepository,
            IPODetailRepository pODetailRepository,
            IProductOrderRepository productOrderRepository,
            IWalletRepository walletRepository,
            ITransactionRepository transactionRepository)
        {
            _dbContext = dbContext;
            _accountRepository = accountRepository;
            _storeRepository = storeRepository;
            _serviceRepository = serviceRepository;
            _roleRepository = roleRepository;
            _productRepository = productRepository;
            _comboRepository = comboRepository;

            _pODetailRepository = pODetailRepository;
            _productOrderRepository = productOrderRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public IAccountRepository AccountRepository => _accountRepository;

        public IStoreRepository StoreRepository => _storeRepository;

        public IComboRepository ComboRepository => _comboRepository;

        public IProductRepository ProductRepository => _productRepository;

        public IRoleRepository RoleRepository => _roleRepository;

        public IServiceRepository ServiceRepository => _serviceRepository;

        public IProductOrderRepository ProductOrderRepository => _productOrderRepository;
        public IPODetailRepository PODetailRepository => _pODetailRepository;
        public IWalletRepository WalletRepository => _walletRepository;
        public ITransactionRepository TransactionRepository => _transactionRepository;

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
