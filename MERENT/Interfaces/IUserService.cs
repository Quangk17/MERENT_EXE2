using Application.ServiceRespones;
using Application.ViewModels.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<List<AccountDTO>>> GetAccountsAsync();
        Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id);
        Task<ServiceResponse<List<AccountDTO>>> SearchAccountByNameAsync(string name);
        Task<ServiceResponse<AccountDTO>> DeleteAccountAsync(int id);
        Task<ServiceResponse<AccountDTO>> UpdateAccountAsync(int id, AccountUpdateDTO updateDto);
        Task<ServiceResponse<AccountDTO>> AddAccountAsync(AccountAddDTO AccountAddDTO);
        Task<ServiceResponse<UserDetailsModel>> GetCurrentUserAsync();
    }

}
