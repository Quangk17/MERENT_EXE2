using Application.Commons;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class ComboRepository :GenericRepository<Combo>, IComboRepository
    {
        private readonly AppDbContext _dbContext;
        public ComboRepository(
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
