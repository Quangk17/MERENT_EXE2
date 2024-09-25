using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ComboDTOs;
using Application.ViewModels.ProductDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComboService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComboService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<ComboDTO>> AddComboAsync(ComboCreateDTO combo)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ComboDTO>> CreateComboAsync(ComboCreateDTO createdto)
        {
            var reponse = new ServiceResponse<ComboDTO>();

            try
            {
                var entity = _mapper.Map<Combo>(createdto);

                await _unitOfWork.ComboRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<ComboDTO>(entity);
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

        public async Task<ServiceResponse<ComboDTO>> DeleteComboAsync(int id)
        {
            var _response = new ServiceResponse<ComboDTO>();
            var court = await _unitOfWork.ComboRepository.GetByIdAsync(id);

            if (court != null)
            {
                _unitOfWork.ComboRepository.SoftRemove(court);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<ComboDTO>(court);
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

        public Task<ServiceResponse<ComboDTO>> GetComboByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ComboDTO>>> GetCombosAsync()
        {
            var response = new ServiceResponse<List<ComboDTO>>();
            List<ComboDTO> CourtDTOs = new List<ComboDTO>();
            try
            {
                var courts = await _unitOfWork.ComboRepository.GetAllAsync();

                foreach (var court in courts)
                {
                    var courtDto = _mapper.Map<ComboDTO>(court);
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

        public Task<ServiceResponse<List<ComboDTO>>> SearchComboByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ComboDTO>> UpdateComboAsync(int id, ComboUpdateDTOs updatedto)
        {
            var reponse = new ServiceResponse<ComboDTO>();

            try
            {
                var enityById = await _unitOfWork.ComboRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updatedto, enityById);
                    var bAfter = _mapper.Map<Combo>(newb);
                    _unitOfWork.ComboRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ComboDTO>(bAfter);
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
