using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.TransactionDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<TransactionDTO>> CreateTransactionAsync(TransactionCreateDTO transaction)
        {
            var response = new ServiceResponse<TransactionDTO>();

            try
            {
                var entity = _mapper.Map<Transaction>(transaction);

                await _unitOfWork.TransactionRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<TransactionDTO>(entity);
                    response.Success = true;
                    response.Message = "Create new Transaction successfully";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create new Transaction failed";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
                return response;
            }

            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> DeleteTransactionAsync(int id)
        {
            var response = new ServiceResponse<TransactionDTO>();
            var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

            if (transaction != null)
            {
                _unitOfWork.TransactionRepository.SoftRemove(transaction);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<TransactionDTO>(transaction);
                    response.Success = true;
                    response.Message = "Deleted Transaction successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete Transaction failed!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Transaction not found";
            }

            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> GetTransactionByIdAsync(int id)
        {
            var response = new ServiceResponse<TransactionDTO>();

            try
            {
                var transaction = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

                if (transaction != null)
                {
                    response.Data = _mapper.Map<TransactionDTO>(transaction);
                    response.Success = true;
                    response.Message = "Transaction retrieved successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Transaction not found!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<TransactionDTO>>> GetTransactionsAsync()
        {
            var response = new ServiceResponse<List<TransactionDTO>>();
            List<TransactionDTO> transactionDTOs = new List<TransactionDTO>();

            try
            {
                var transactions = await _unitOfWork.TransactionRepository.GetAllAsync();

                foreach (var transaction in transactions)
                {
                    var transactionDto = _mapper.Map<TransactionDTO>(transaction);
                    transactionDTOs.Add(transactionDto);
                }

                if (transactionDTOs.Count > 0)
                {
                    response.Data = transactionDTOs;
                    response.Success = true;
                    response.Message = $"Retrieved {transactionDTOs.Count} transactions.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No transactions found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<TransactionDTO>> UpdateTransactionAsync(int id, TransactionUpdateDTO updateDto)
        {
            var response = new ServiceResponse<TransactionDTO>();

            try
            {
                var entityById = await _unitOfWork.TransactionRepository.GetByIdAsync(id);

                if (entityById != null)
                {
                    var updatedEntity = _mapper.Map(updateDto, entityById);
                    _unitOfWork.TransactionRepository.Update(updatedEntity);

                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        response.Data = _mapper.Map<TransactionDTO>(updatedEntity);
                        response.Success = true;
                        response.Message = "Transaction updated successfully!";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Update transaction failed!";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Transaction not found!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

    }
}
