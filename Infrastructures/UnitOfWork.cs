
using Arch.EntityFrameworkCore.UnitOfWork;
using Application;


namespace Infrastructures
{
    public class UnitOfWork : Application.IUnitOfWork
    {
        private readonly AppDBContext _dbContext;


        //

        public UnitOfWork(AppDBContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        //

        public async Task<int> SaveChangeAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
