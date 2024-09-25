using Application.Commons;
using Application.Repositories;
using Domain.Entites;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class ComboRepository : IComboRepository
    {
        public ComboRepository()
        {
            
        }
        public Task AddAsync(Combo entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<Combo> entities)
        {
            throw new NotImplementedException();
        }

        public Task<List<Combo>> GetAllAsync(params Expression<Func<Combo, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Combo?> GetByIdAsync(int id, params Expression<Func<Combo, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public void SoftRemove(Combo entity)
        {
            throw new NotImplementedException();
        }

        public void SoftRemoveRange(List<Combo> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<Combo>> ToPagination(int pageNumber = 0, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public void Update(Combo entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<Combo> entities)
        {
            throw new NotImplementedException();
        }
    }
}
