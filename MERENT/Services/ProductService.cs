using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.ServiceDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductDTO>> CreateProductAsync(ProductCreateDTO createdto)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var entity = _mapper.Map<Product>(createdto);

                await _unitOfWork.ProductRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<ProductDTO>(entity);
                    reponse.Success = true;
                    reponse.Message = "Create new Court successfully";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = "Create new Court fail";
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

        public async Task<ServiceResponse<ProductDTO>> DeleteProductAsync(int id)
        {
            var _response = new ServiceResponse<ProductDTO>();
            var court = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (court != null)
            {
                _unitOfWork.ProductRepository.SoftRemove(court);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<ProductDTO>(court);
                    _response.Success = true;
                    _response.Message = "Deleted Court Successfully!";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Deleted Court Fail!";
                }
            }
            else
            {
                _response.Success = false;
                _response.Message = "Court not found";
            }

            return _response;
        }

        public Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<ProductDTO>>();
            List<ProductDTO> CourtDTOs = new List<ProductDTO>();
            try
            {
                var courts = await _unitOfWork.ProductRepository.GetAllAsync();

                foreach (var court in courts)
                {
                    var courtDto = _mapper.Map<ProductDTO>(court);
                    courtDto.Name = court.Name;
                    CourtDTOs.Add(courtDto);
                }
                if (CourtDTOs.Count > 0)
                {
                    response.Data = CourtDTOs;
                    response.Success = true;
                    response.Message = $"Have {CourtDTOs.Count} roles.";
                    response.Error = "No error";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No roles found.";
                    response.Error = "No roles available";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = "Exception";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;

        }

        public Task<ServiceResponse<List<ProductDTO>>> SearchProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ProductDTO>> UpdateProductAsync(int id, ProductUpdateDTO updatedto)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var enityById = await _unitOfWork.ProductRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updatedto, enityById);
                    var bAfter = _mapper.Map<Product>(newb);
                    _unitOfWork.ProductRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ProductDTO>(bAfter);
                        reponse.Message = $"Successfull for update Court.";
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
                    reponse.Message = $"Have no Court.";
                    reponse.Error = "Not have a Court";
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

        public async Task<ServiceResponse<ProductDTO>> UploadImageProductAsync(int id, UploadImageDTO uploadImageDTO)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var enityById = await _unitOfWork.ProductRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(uploadImageDTO, enityById);
                    var bAfter = _mapper.Map<Product>(newb);
                    _unitOfWork.ProductRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ProductDTO>(bAfter);
                        reponse.Message = $"Successfull for update Product.";
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
                    reponse.Message = $"Have no Product.";
                    reponse.Error = "Not have a Product";
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
