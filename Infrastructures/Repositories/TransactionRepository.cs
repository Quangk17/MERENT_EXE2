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
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly AppDbContext _dbContext;
        public TransactionRepository(
            AppDbContext context,
            ICurrentTime timeService,
            IClaimsService claimsService
        )
            : base(context, timeService, claimsService)
        {
            _dbContext = context;
        }

        public async Task<List<Transaction>> GetTransactionsByUserId(int userId, string walletRequestTypeEnums)
        {
            if (walletRequestTypeEnums == WalletRequestTypeEnums.ALL.ToString())
            {
                return await _dbContext.Transactions.Where(x => x.Wallets.UserId == userId).ToListAsync();
            }
            else if (walletRequestTypeEnums == WalletRequestTypeEnums.PERSONAL.ToString())
            {
                return await _dbContext.Transactions.Where(x => x.Wallets.UserId == userId && x.Wallets.WalletType == WalletRequestTypeEnums.PERSONAL.ToString()).ToListAsync();
            }
            else if (walletRequestTypeEnums == WalletRequestTypeEnums.ORGANIZATIONAL.ToString())
            {
                return await _dbContext.Transactions.Where(x => x.Wallets.UserId == userId && x.Wallets.WalletType == WalletRequestTypeEnums.ORGANIZATIONAL.ToString()).ToListAsync();
            }
            else
            {
                throw new Exception("Invalid wallet type");
            }

        }
    }
}

