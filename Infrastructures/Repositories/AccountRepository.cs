using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructures.Repositories
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        public AccountRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }




        public Task<User> CheckEmailNameExisted(string email)
        => _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<bool> CheckPhoneNumberExisted(string phonenumber)
        => _dbContext.Users.AnyAsync(u => u.PhoneNumber == phonenumber);

        public async Task<IEnumerable<User>> GetAccountsAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Role> GetRoleNameByRoleId(int RoleId)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == RoleId);
        }

        public Task<IEnumerable<User>> GetSortedAccountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByConfirmationToken(string token)
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("DbContext is not initialized.");
            }
            var users = _dbContext.Users.ToList();
            Console.WriteLine(users);
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.ConfirmationToken == token);

            if (user == null)
            {
                throw new KeyNotFoundException($"No user found with the provided confirmation token: {token}");
            }

            return user;

        }

        public Task<User> GetUserByEmailAndPasswordHash(string email, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> SearchAccountByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> SearchAccountByRoleNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
