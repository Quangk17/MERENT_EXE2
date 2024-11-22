using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IPODetailRepository: IGenericRepository<ProductOrderDetails>
    {
        Task<List<ProductOrderDetails>> GetAllPODetailsAsync(
        Expression<Func<ProductOrderDetails, object>>? include = null
    );
    }
}
