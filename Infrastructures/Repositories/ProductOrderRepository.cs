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
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductOrderRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ProductOrder>> GetProductOrdersByUserIdAsync(int userId)
        {
            return await _dbContext.ProductOrders
                .Where(order => order.UserID == userId)
                .ToListAsync();
        }

        public async Task<ProductOrder?> GetLatestProductOrderByUserIdAsync(int userId)
        {
            return await _dbContext.ProductOrders
                .Where(order => order.UserID == userId)
                .OrderByDescending(order => order.CreationDate)
                .FirstOrDefaultAsync();
        }


    }
}
