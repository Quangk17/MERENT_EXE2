using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.StoreDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<StoreDTO>> CreateStoreAsync(StoreCreateDTO store)
        {
            var reponse = new ServiceResponse<StoreDTO>();

            try
            {
                var entity = _mapper.Map<Store>(store);

                await _unitOfWork.StoreRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<StoreDTO>(entity);
                    reponse.Success = true;
                    reponse.Message = "Create new Store successfully";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = "Create new Store fail";
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

        public async Task<ServiceResponse<StoreDTO>> DeleteStoreAsync(int id)
        {
            var _response = new ServiceResponse<StoreDTO>();
            var store = await _unitOfWork.StoreRepository.GetByIdAsync(id);

            if (store != null)
            {
                _unitOfWork.StoreRepository.SoftRemove(store);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<StoreDTO>(store);
                    _response.Success = true;
                    _response.Message = "Deleted store Successfully!";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Deleted store Fail!";
                }
            }
            else
            {
                _response.Success = false;
                _response.Message = "Store not found";
            }

            return _response;
        }

        public Task<ServiceResponse<StoreDTO>> GetStoreByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<StoreDTO>>> GetStoresAsync()
        {
            var reponse = new ServiceResponse<List<StoreDTO>>();
            List<StoreDTO> StoreDTOs = new List<StoreDTO>();
            try
            {
                var a = await _unitOfWork.StoreRepository.GetAllAsync();
                foreach (var ac in a)
                {
                    var aaftermapper = _mapper.Map<StoreDTO>(ac);
                    aaftermapper.Name = ac.Name;
                    StoreDTOs.Add(aaftermapper);
                }
                if (StoreDTOs.Count > 0)
                {
                    reponse.Data = StoreDTOs;
                    reponse.Success = true;
                    reponse.Message = $"Have {StoreDTOs.Count} stores.";
                    reponse.Error = "Not error";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = $"Have {StoreDTOs.Count} stores.";
                    reponse.Error = "Not have a store";
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

        public async Task<ServiceResponse<List<StoreDTO>>> SearchStoreByNameAsync(string name)
        {
            var _response = new ServiceResponse<List<StoreDTO>>();
            List<StoreDTO> storeDTOs = new List<StoreDTO>();

            try
            {
                var store = await _unitOfWork.StoreRepository.GetAllAsync();
                var storeFilter = store.Where(x => x.Name.ToLower().Contains(name.ToLower()));
                if (storeFilter != null)
                {
                    foreach (var a in storeFilter)
                    {
                        if (a.IsDeleted == false)
                        {
                            storeDTOs.Add(_mapper.Map<StoreDTO>(a));
                        }
                    }
                    if (storeDTOs.Any() == true)
                    {
                        _response.Data = storeDTOs;
                        _response.Success = true;
                        _response.Message = "Found Store By Name";
                    }
                    else
                    {
                        _response.Success = false;
                        _response.Message = "Not Found Store";
                    }
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Don't Have Any Store ";
                }

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        public async Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(int id, StoreUpdateDTO updateDto)
        {
            var reponse = new ServiceResponse<StoreDTO>();

            try
            {
                var enityById = await _unitOfWork.StoreRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updateDto, enityById);
                    var bAfter = _mapper.Map<Store>(newb);
                    _unitOfWork.StoreRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<StoreDTO>(bAfter);
                        reponse.Message = $"Successfull for update Store.";
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
                    reponse.Message = $"Have no Store.";
                    reponse.Error = "Not have a Store";
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
