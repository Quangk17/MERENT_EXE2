using Application.ServiceRespones;
using Application.ViewModels.WalletDTOs;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWalletService
    {
        Task<ServiceResponse<List<WalletDTO>>> GetWalletsAsync();
        Task<ServiceResponse<WalletDTO>> GetWalletByIdAsync(int id);
        Task<ServiceResponse<WalletDTO>> DeleteWalletAsync(int id);
        Task<ServiceResponse<WalletDTO>> UpdateWalletAsync(int id, WalletUpdateDTO updateDto);
        Task<ServiceResponse<WalletDTO>> CreateWalletAsync(WalletCreateDTO wallet);
        Task<ServiceResponse<WalletDTO>> CreateWalletForUserAsync(WalletCreateDTO wallet);
        Task<List<WalletDTO>> GetWalletByUserId(int userId);
        Task<Transaction> Deposit(int userId, long amount, string? paymentMethod);
        Task<ServiceResponse<WalletDTO>> RefundToWalletAsync(int userId, long amount);

        Task<int?> GetWalletIdForUserAsync(int userId);
    }
}
