using Application.Interfaces;
using Application.Repositories;
using Domain.Entites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class WalletRepository: GenericRepository<Wallet>, IWalletRepository
    {
        private readonly AppDbContext _dbContext;
        public WalletRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<Transaction> DepositMoney(int userId, WalletTypeEnums walletType, long amount, string? paymentMethod = "VNPay")
        {
            var wallet = await GetWalletByUserIdAndType(userId, walletType);
            if (wallet == null)
            {
                throw new Exception("Wallet not found");
            }

            //Create new transaction
            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                PaymentType = TransactionTypeEnums.DEPOSIT.ToString(),
                TotalAmount = amount,
                Description = "Deposit money with amount: " + amount + ", Thanh toán qua: " + paymentMethod,
                CreationDate = DateTime.UtcNow,
                Status = TransactionStatusEnums.PENDING.ToString()
            };
            //add transaction to database
            await _dbContext.Transactions.AddAsync(transaction);

            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<Wallet> GetWalletByUserIdAndType(int userId, WalletTypeEnums walletType)
        {
            var wallets = await GetListWalletByUserId(userId);
            var wallet = await _dbContext.Wallets.FirstOrDefaultAsync(x => x.UserId == userId && x.WalletType.ToUpper() == walletType.ToString().ToUpper());
            return wallet;
        }

        public async Task<List<Wallet>> GetListWalletByUserId(int userId)
        {
            //check user is exist
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            //get list wallet by userId
            var wallets = await _dbContext.Wallets.Where(x => x.UserId == userId).ToListAsync();

            //check if wallet with type personal is null, create new personal wallet
            /*var personalWallet = wallets.FirstOrDefault(x => x.WalletType.ToUpper() == WalletTypeEnums.PERSONAL.ToString().ToUpper());
            if (personalWallet == null)
            {
                var newPersonalWallet = new Wallet
                {
                    UserId = userId,
                    WalletType = WalletTypeEnums.PERSONAL.ToString(),
                    Cash = 0
                };
                await AddAsync(newPersonalWallet);
                wallets.Add(newPersonalWallet);
            }

            //check if wallet with type organization is null, create new organization wallet
            var organizationWallet = wallets.FirstOrDefault(x => x.WalletType.ToUpper() == WalletTypeEnums.ORGANIZATIONAL.ToString().ToUpper());
            if (organizationWallet == null)
            {
                var newOrganizationWallet = new Wallet
                {
                    UserId = userId,
                    WalletType = WalletTypeEnums.ORGANIZATIONAL.ToString(),
                    Cash = 0
                };
                await AddAsync(newOrganizationWallet);
                wallets.Add(newOrganizationWallet);
            }

            await _dbContext.SaveChangesAsync();*/

            return wallets;
        }

        public async Task<Transaction?> GetLatestTransactionByWalletIdAsync(int walletId)
        {
            if (walletId <= 0)
            {
                throw new ArgumentException("WalletId must be greater than 0.", nameof(walletId));
            }

            // Truy vấn giao dịch mới nhất từ walletId
            var latestTransaction = await _dbContext.Transactions
                .Where(t => t.WalletId == walletId)
                .OrderByDescending(t => t.CreationDate) // Sắp xếp giảm dần theo thời gian tạo
                .FirstOrDefaultAsync();

            return latestTransaction;
        }

        public async Task<int?> GetWalletIdByUserIdAsync(int userId)
        {
            // Lấy ví đầu tiên của user dựa trên userId
            var wallet = await _dbContext.Wallets
                .Where(w => w.UserId == userId)
                .FirstOrDefaultAsync();

            return wallet?.Id; // Trả về walletId nếu ví tồn tại, ngược lại trả về null
        }
    }
}

