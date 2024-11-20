using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ComboProductDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComboOfProductService : IComboOfProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComboOfProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ComboOfProductDTO>>> GetCombosAsync()
        {
            var response = new ServiceResponse<List<ComboOfProductDTO>>();
            try
            {
                var entities = await _unitOfWork.ComboOfProductRepository.GetAllAsync();
                response.Data = entities.Select(e => _mapper.Map<ComboOfProductDTO>(e)).ToList();
                response.Success = true;
                response.Message = "Fetched all ComboOfProducts successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ComboOfProductDTO>> GetComboByIdAsync(int id)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                response.Success = true;
                response.Message = "Fetched ComboOfProduct successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ComboOfProductDTO>>> SearchComboByNameAsync(string name)
        {
            var response = new ServiceResponse<List<ComboOfProductDTO>>();
            try
            {
                var entities = await _unitOfWork.ComboOfProductRepository.GetAllAsync(x => x.Combo.Name.Contains(name));
                response.Data = entities.Select(e => _mapper.Map<ComboOfProductDTO>(e)).ToList();
                response.Success = true;
                response.Message = $"Found {response.Data.Count} ComboOfProducts matching '{name}'.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ComboOfProductDTO>> DeleteComboAsync(int id)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                _unitOfWork.ComboOfProductRepository.SoftRemove(entity);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Deleted ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to delete ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        /*public async Task<ServiceResponse<ComboOfProductDTO>> UpdateComboAsync(int id1, int id2, ComboOfProductUpdateDTO updateDto)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(new { ComboID = id1, ProductID = id2 });
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                _mapper.Map(updateDto, entity);
                _unitOfWork.ComboOfProductRepository.Update(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Updated ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to update ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }*/

        public async Task<ServiceResponse<ComboOfProductDTO>> CreateComboAsync(ComboOfProductCreateDTO createDto)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = _mapper.Map<ComboOfProduct>(createDto);
                await _unitOfWork.ComboOfProductRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Created ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to create ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ComboWithProductsDTO>>> GetCombosWithProductsAsync()
        {
            var response = new ServiceResponse<List<ComboWithProductsDTO>>();

            try
            {
                var combos = await _unitOfWork.ComboRepository.GetAllAsync(c => c.ComboOfProducts);

                var result = combos.Select(combo => new ComboWithProductsDTO
                {
                    ComboID = combo.Id,
                    ProductIDs = combo.ComboOfProducts.Select(cop => cop.ProductID).ToList()
                }).ToList();

                response.Data = result;
                response.Success = true;
                response.Message = "Fetched all combos with products successfully.";
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
