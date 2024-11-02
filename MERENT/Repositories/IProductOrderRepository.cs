using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IProductOrderRepository: IGenericRepository<ProductOrder>
    {
        Task<IEnumerable<ProductOrder>> GetProductOrdersByUserIdAsync(int userId);
        Task<ProductOrder?> GetLatestProductOrderByUserIdAsync(int userId);
    }
}
