using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.AccountDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDetailsModel>> GetCurrentUserAsync()
        {
            var response = new ServiceResponse<UserDetailsModel>();

            try
            {
                var currentUser = await _unitOfWork.AccountRepository.GetCurrentUserAsync();

                if (currentUser != null)
                {
                    var role = await _unitOfWork.AccountRepository.GetRole(currentUser);
                    var data = _mapper.Map<UserDetailsModel>(currentUser);
                    data.Role = role;

                    response.Success = true;
                    response.Data = data;
                    response.Message = "Successfully retrieved current user.";
                    return response;
                }
                else
                {
                    response.Success = false;
                    response.Message = "User not found.";
                    response.Error = "User is not found due to error or expiration token.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
                return response;
            }
        }


        public async Task<ServiceResponse<AccountDTO>> AddAccountAsync(AccountAddDTO AccountAddDTO)
        {
            var reponse = new ServiceResponse<AccountDTO>();

            try
            {
                var entity = _mapper.Map<User>(AccountAddDTO);

                await _unitOfWork.AccountRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<AccountDTO>(entity);
                    reponse.Success = true;
                    reponse.Message = "Create new Account successfully";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = "Create new Account fail";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }

            return reponse;
        }

        public async Task<ServiceResponse<AccountDTO>> DeleteAccountAsync(int id)
        {
            var _response = new ServiceResponse<AccountDTO>();
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);

            if (account != null)
            {
                _unitOfWork.AccountRepository.SoftRemove(account);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<AccountDTO>(account);
                    _response.Success = true;
                    _response.Message = "Deleted Account Successfully!";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Deleted Account Fail!";
                }
            }
            else
            {
                _response.Success = false;
                _response.Message = "account not found";
            }

            return _response;
        }

        public async Task<ServiceResponse<AccountDTO>> GetAccountByIdAsync(int id)
        {
            var _response = new ServiceResponse<AccountDTO>();


            try
            {
                var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
                if (account != null)
                {
                    var productDTO = _mapper.Map<AccountDTO>(account);


                    _response.Data = productDTO;
                    _response.Success = true;
                    _response.Message = "Found Account By Id";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Don't Have Any Account ";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        public async Task<ServiceResponse<List<AccountDTO>>> GetAccountsAsync()
        {
            var reponse = new ServiceResponse<List<AccountDTO>>();
            List<AccountDTO> UserDTOs = new List<AccountDTO>();
            try
            {
                var a = await _unitOfWork.AccountRepository.GetAllAsync(x => x.Role);
                foreach (var ac in a)
                {
                    var aaftermapper = _mapper.Map<AccountDTO>(ac);
                    aaftermapper.RoleID = ac.Role.Id;
                    UserDTOs.Add(aaftermapper);
                }
                if (UserDTOs.Count > 0)
                {
                    reponse.Data = UserDTOs;
                    reponse.Success = true;
                    reponse.Message = $"Have {UserDTOs.Count} account.";
                    reponse.Error = "Not error";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = $"Have {UserDTOs.Count} account.";
                    reponse.Error = "Not have a order";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.Error = "Exception";
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }
        }

        public async Task<ServiceResponse<List<AccountDTO>>> SearchAccountByNameAsync(string name)
        {
            var _response = new ServiceResponse<List<AccountDTO>>();
            List<AccountDTO> accountDTOs = new List<AccountDTO>();

            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetAccountsAsync();
                var accountFilter = accounts.Where(x => x.Email.ToLower().Contains(name.ToLower())
                                    || x.Name.ToLower().Contains(name.ToLower()));
                if (accountFilter != null)
                {
                    foreach (var a in accountFilter)
                    {
                        if (a.IsDeleted == false)
                        {
                            accountDTOs.Add(_mapper.Map<AccountDTO>(a));
                        }
                    }
                    if (accountDTOs.Any() == true)
                    {
                        _response.Data = accountDTOs;
                        _response.Success = true;
                        _response.Message = "Found Account By Name";
                    }
                    else
                    {
                        _response.Success = false;
                        _response.Message = "Not Found Account";
                    }
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Don't Have Any Account ";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        public async Task<ServiceResponse<AccountDTO>> UpdateAccountAsync(int id, AccountUpdateDTO updateDto)
        {
            var reponse = new ServiceResponse<AccountDTO>();

            try
            {
                var accountById = await _unitOfWork.AccountRepository.GetByIdAsync(id);

                if (accountById != null)
                {
                    var newAccount = _mapper.Map(updateDto, accountById);
                    var accountAfter = _mapper.Map<User>(newAccount);
                    _unitOfWork.AccountRepository.Update(newAccount);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<AccountDTO>(accountAfter);
                        reponse.Message = $"Successfull for update account.";
                        return reponse;
                    }
                    else
                    {
                        reponse.Success = false;
                        reponse.Error = "Save update failed";
                        return reponse;
                    }

                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = $"Have no account.";
                    reponse.Error = "Not have a account";
                    return reponse;
                }

            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }

            return reponse;
        }
    }
}
