using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class PODetailRepository : GenericRepository<ProductOrderDetails>, IPODetailRepository
    {
        private readonly AppDbContext _dbContext;
        public PODetailRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<ProductOrderDetails>> GetAllPODetailsAsync(Expression<Func<ProductOrderDetails, object>>? include = null)
        {
            IQueryable<ProductOrderDetails> query = _dbSet;

            if (include != null)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
    }
}
