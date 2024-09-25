using Application.Interfaces;
using Application.ServiceRespones;
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
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ServiceDTO>> CreateServiceAsync(ServiceCreateDTO createdto)
        {
            var reponse = new ServiceResponse<ServiceDTO>();

            try
            {
                var entity = _mapper.Map<Service>(createdto);

                await _unitOfWork.ServiceRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<ServiceDTO>(entity);
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

        public async Task<ServiceResponse<ServiceDTO>> DeleteServiceAsync(int id)
        {
            var _response = new ServiceResponse<ServiceDTO>();
            var court = await _unitOfWork.ServiceRepository.GetByIdAsync(id);

            if (court != null)
            {
                _unitOfWork.ServiceRepository.SoftRemove(court);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<ServiceDTO>(court);
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

        public Task<ServiceResponse<ServiceDTO>> GetServiceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ServiceDTO>>> GetServicesAsync()
        {
            var response = new ServiceResponse<List<ServiceDTO>>();
            List<ServiceDTO> CourtDTOs = new List<ServiceDTO>();
            try
            {
                var courts = await _unitOfWork.ServiceRepository.GetAllAsync();

                foreach (var court in courts)
                {
                    var courtDto = _mapper.Map<ServiceDTO>(court);
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

        public Task<ServiceResponse<List<ServiceDTO>>> SearchServiceByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ServiceDTO>> UpdateServiceAsync(int id, ServiceUpdateDTO updatedto)
        {
            var reponse = new ServiceResponse<ServiceDTO>();

            try
            {
                var enityById = await _unitOfWork.ServiceRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updatedto, enityById);
                    var bAfter = _mapper.Map<Service>(newb);
                    _unitOfWork.ServiceRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ServiceDTO>(bAfter);
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
    }
}
