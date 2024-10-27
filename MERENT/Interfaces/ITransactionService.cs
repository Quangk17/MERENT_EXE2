using Application.ServiceRespones;
using Application.ViewModels.TransactionDTOs;
using Application.ViewModels.WalletDTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResponse<List<TransactionDTO>>> GetTransactionsAsync();
        Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(int id);
        Task<ServiceResponse<TransactionDTO>> DeleteTransactionAsync(int id);
        Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(int id, TransactionUpdateDTO updateDto);
        Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(TransactionCreateDTO transaction);
        Task<List<TransactionResponsesDTO>> GetTransactionsByUserId(int userId, WalletRequestTypeEnums walletRequestTypeEnums);

    }
}
