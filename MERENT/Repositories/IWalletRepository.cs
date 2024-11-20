using Application.ViewModels.WalletDTOs;
using Domain.Entites;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IWalletRepository : IGenericRepository<Wallet>
    {
        Task<Transaction> DepositMoney(int userId, WalletTypeEnums walletType, long amount, string? paymentMethod);
        Task<Wallet> GetWalletByUserIdAndType(int userId, WalletTypeEnums walletType);
        Task<List<Wallet>> GetListWalletByUserId(int userId);
        Task<Transaction?> GetLatestTransactionByWalletIdAsync(int walletId);

        Task<int?> GetWalletIdByUserIdAsync(int userId);
    }
}
