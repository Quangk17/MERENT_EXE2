using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.WalletDTOs;
using AutoMapper;
using Domain.Entites;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WalletService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<WalletDTO>> CreateWalletAsync(WalletCreateDTO wallet)
        {
            var response = new ServiceResponse<WalletDTO>();

            try
            {
                var entity = _mapper.Map<Wallet>(wallet);

                await _unitOfWork.WalletRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<WalletDTO>(entity);
                    response.Success = true;
                    response.Message = "Create new Wallet successfully";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create new Wallet failed";
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

        public async Task<ServiceResponse<WalletDTO>> DeleteWalletAsync(int id)
        {
            var response = new ServiceResponse<WalletDTO>();
            var wallet = await _unitOfWork.WalletRepository.GetByIdAsync(id);

            if (wallet != null)
            {
                _unitOfWork.WalletRepository.SoftRemove(wallet);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<WalletDTO>(wallet);
                    response.Success = true;
                    response.Message = "Deleted Wallet successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Delete Wallet failed!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "Wallet not found";
            }

            return response;
        }

        public async Task<ServiceResponse<WalletDTO>> GetWalletByIdAsync(int id)
        {
            var response = new ServiceResponse<WalletDTO>();

            try
            {
                var wallet = await _unitOfWork.WalletRepository.GetByIdAsync(id);

                if (wallet != null)
                {
                    response.Data = _mapper.Map<WalletDTO>(wallet);
                    response.Success = true;
                    response.Message = "Wallet retrieved successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Wallet not found!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<WalletDTO>>> GetWalletsAsync()
        {
            var response = new ServiceResponse<List<WalletDTO>>();
            List<WalletDTO> walletDTOs = new List<WalletDTO>();

            try
            {
                var wallets = await _unitOfWork.WalletRepository.GetAllAsync();

                foreach (var wallet in wallets)
                {
                    var walletDto = _mapper.Map<WalletDTO>(wallet);
                    walletDTOs.Add(walletDto);
                }

                if (walletDTOs.Count > 0)
                {
                    response.Data = walletDTOs;
                    response.Success = true;
                    response.Message = $"Retrieved {walletDTOs.Count} wallets.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No wallets found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<WalletDTO>> UpdateWalletAsync(int id, WalletUpdateDTO updateDto)
        {
            var response = new ServiceResponse<WalletDTO>();

            try
            {
                var entityById = await _unitOfWork.WalletRepository.GetByIdAsync(id);

                if (entityById != null)
                {
                    var updatedEntity = _mapper.Map(updateDto, entityById);
                    _unitOfWork.WalletRepository.Update(updatedEntity);

                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        response.Data = _mapper.Map<WalletDTO>(updatedEntity);
                        response.Success = true;
                        response.Message = "Wallet updated successfully!";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Update wallet failed!";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "Wallet not found!";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }



        public async Task<Transaction> Deposit(int userId, long amount, string? paymentMethod)
        {
            var transaction = await _unitOfWork.WalletRepository.DepositMoney(userId, WalletTypeEnums.PERSONAL, amount, paymentMethod);
            var result = _mapper.Map<Transaction>(transaction);
            return result;
        }

        public async Task<List<WalletDTO>> GetWalletByUserId(int userId)
        {
            var wallets = await _unitOfWork.WalletRepository.GetListWalletByUserId(userId);
            var result = _mapper.Map<List<WalletDTO>>(wallets);
            return result;
        }

        public async Task<ServiceResponse<WalletDTO>> RefundToWalletAsync(int userId, long amount)
        {
            var response = new ServiceResponse<WalletDTO>();
            try
            {
                // Lấy ví của người dùng
                var wallet = await _unitOfWork.WalletRepository.GetWalletByUserIdAndType(userId, WalletTypeEnums.PERSONAL);
                if (wallet == null)
                {
                    response.Success = false;
                    response.Message = "Wallet not found for the specified user.";
                    return response;
                }

                // Cập nhật số tiền
                wallet.Cash += amount;
                _unitOfWork.WalletRepository.Update(wallet);

                // Lưu thay đổi
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<WalletDTO>(wallet);
                    response.Success = true;
                    response.Message = $"Successfully refunded {amount} to the wallet.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to refund money to the wallet.";
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
