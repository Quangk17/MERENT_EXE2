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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;
        public ProductRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Product>> GetAllProductsByIdsAsync(List<int> productIds)
        {
            // Truy vấn sản phẩm và tải thêm thuộc tính dẫn hướng nếu cần
            return await _dbContext.Products
                .Where(p => productIds.Contains(p.Id)) // Lọc dựa trên ProductID
                .ToListAsync();
        }

        public IQueryable<Product> Query()
        {
            return _dbContext.Products.AsQueryable();
        }

        public async Task<List<Product>> GetAllProductsByIds2Async(List<int> productIds)
        {
            return await _dbSet.Where(product => productIds.Contains(product.Id)).ToListAsync();
        }

    }
}
