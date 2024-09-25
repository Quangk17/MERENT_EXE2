using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;


namespace Infrastructures.Repositories
{
    public class ServiceRepository: GenericRepository<Service>, IServiceRepository
    {
        private readonly AppDbContext _dbContext;
        public ServiceRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }
    }
}
