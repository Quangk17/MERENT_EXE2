using Application.Interfaces;
using Application.Repositories;
using Application.ViewModels.AccountDTOs;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructures.Repositories
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IClaimsService _claimsService;
        public AccountRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
            _claimsService = claimsService;
        }




        public Task<User> CheckEmailNameExisted(string email)
        => _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        public Task<bool> CheckPhoneNumberExisted(string phonenumber)
        => _dbContext.Users.AnyAsync(u => u.PhoneNumber == phonenumber);

        public async Task<IEnumerable<User>> GetAccountsAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _claimsService.GetCurrentUserId);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<RoleInfoModel> GetRole(User user)
        {
            // Giả sử bạn có một bảng `UserRoles` trong cơ sở dữ liệu lưu thông tin người dùng và vai trò của họ
            // Bạn có thể lấy vai trò của user từ bảng `UserRoles` và sau đó lấy thông tin của vai trò từ `Role` bảng.

            var userRole = await _dbContext.Roles.FirstOrDefaultAsync(ur => ur.Id == user.Id);
            if (userRole == null)
            {
                throw new Exception("User does not have a role assigned.");
            }

            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == userRole.Id);
            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            // Trả về thông tin vai trò
            return new RoleInfoModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
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
