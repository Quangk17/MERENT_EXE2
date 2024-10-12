using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ProductOrderDetailDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PODetailService :IPODetailService
    {
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PODetailService(
        IUnitOfWork unitOfWork,
        IMapper mapper
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

        public async Task<ServiceResponse<PODetailDTO>> CreatePODetailAsync(PODetailCreateDTO poDetail)
        {
            var response = new ServiceResponse<PODetailDTO>();

            try
            {
                var entity = _mapper.Map<ProductOrderDetails>(poDetail);

                await _unitOfWork.PODetailRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<PODetailDTO>(entity);
                    response.Success = true;
                    response.Message = "Created new PODetail successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to create new PODetail";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<PODetailDTO>> DeletePODetailAsync(int id)
        {
            var response = new ServiceResponse<PODetailDTO>();
            var poDetail = await _unitOfWork.PODetailRepository.GetByIdAsync(id);

            if (poDetail != null)
            {
                _unitOfWork.PODetailRepository.SoftRemove(poDetail);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<PODetailDTO>(poDetail);
                    response.Success = true;
                    response.Message = "Deleted PODetail successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to delete PODetail!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "PODetail not found";
            }

            return response;
        }

        public async Task<ServiceResponse<PODetailDTO>> GetPODetailByIdAsync(int id)
        {
            var response = new ServiceResponse<PODetailDTO>();
            var poDetail = await _unitOfWork.PODetailRepository.GetByIdAsync(id);

            if (poDetail != null)
            {
                response.Data = _mapper.Map<PODetailDTO>(poDetail);
                response.Success = true;
                response.Message = "PODetail found";
            }
            else
            {
                response.Success = false;
                response.Message = "PODetail not found";
            }

            return response;
        }

        public async Task<ServiceResponse<List<PODetailDTO>>> GetPODetailsAsync()
        {
            var response = new ServiceResponse<List<PODetailDTO>>();
            var poDetails = await _unitOfWork.PODetailRepository.GetAllAsync();
            var poDetailDTOs = poDetails.Select(poDetail => _mapper.Map<PODetailDTO>(poDetail)).ToList();

            if (poDetailDTOs.Any())
            {
                response.Data = poDetailDTOs;
                response.Success = true;
                response.Message = $"Found {poDetailDTOs.Count} PODetails.";
            }
            else
            {
                response.Success = false;
                response.Message = "No PODetails found.";
            }

            return response;
        }

       /* public async Task<ServiceResponse<List<PODetailDTO>>> SearchPODetailByNameAsync(string name)
        {
            var response = new ServiceResponse<List<PODetailDTO>>();
            var poDetails = await _unitOfWork.PODetailRepository.SearchByNameAsync(name);
            var poDetailDTOs = poDetails.Select(poDetail => _mapper.Map<PODetailDTO>(poDetail)).ToList();

            if (poDetailDTOs.Any())
            {
                response.Data = poDetailDTOs;
                response.Success = true;
                response.Message = $"Found {poDetailDTOs.Count} PODetails matching '{name}'.";
            }
            else
            {
                response.Success = false;
                response.Message = $"No PODetails found matching '{name}'.";
            }

            return response;
        }*/

        public async Task<ServiceResponse<PODetailDTO>> UpdatePODetailAsync(int id, PODetailUpdateDTO updateDto)
        {
            var response = new ServiceResponse<PODetailDTO>();

            try
            {
                var entityById = await _unitOfWork.PODetailRepository.GetByIdAsync(id);

                if (entityById != null)
                {
                    var updatedEntity = _mapper.Map(updateDto, entityById);
                    _unitOfWork.PODetailRepository.Update(updatedEntity);

                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        response.Success = true;
                        response.Data = _mapper.Map<PODetailDTO>(updatedEntity);
                        response.Message = "PODetail updated successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Failed to update PODetail";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "PODetail not found";
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
