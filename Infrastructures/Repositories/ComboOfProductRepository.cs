using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class ComboOfProductRepository : GenericRepository<ComboOfProduct>, IComboOfProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ComboOfProductRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Combo>> GetAllCombosWithProductsAsync()
        {
            return await _dbContext.Combos
                .Include(c => c.ComboOfProducts)
                .ThenInclude(cop => cop.Product)
                .ToListAsync();
        }

        public async Task<Combo?> GetComboWithProductsByIdAsync(int comboId)
        {
            return await _dbContext.Combos
                .Include(c => c.ComboOfProducts)
                .ThenInclude(cop => cop.Product)
                .FirstOrDefaultAsync(c => c.Id == comboId);
        }

    }
}
