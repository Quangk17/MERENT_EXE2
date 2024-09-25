using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.RoleDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<RoleDTO>> CreateRoleAsync(RoleCreateDTO role)
        {
            var reponse = new ServiceResponse<RoleDTO>();

            try
            {
                var entity = _mapper.Map<Role>(role);

                await _unitOfWork.RoleRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<RoleDTO>(entity);
                    reponse.Success = true;
                    reponse.Message = "Create new Role successfully";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = "Create new Role fail";
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

        public async Task<ServiceResponse<RoleDTO>> DeleteRoleAsync(int id)
        {
            var _response = new ServiceResponse<RoleDTO>();
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(id);

            if (role != null)
            {
                _unitOfWork.RoleRepository.SoftRemove(role);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<RoleDTO>(role);
                    _response.Success = true;
                    _response.Message = "Deleted Role Successfully!";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Deleted Role Fail!";
                }
            }
            else
            {
                _response.Success = false;
                _response.Message = "Role not found";
            }

            return _response;
        }

        public async Task<ServiceResponse<List<RoleDTO>>> GetRolesAsync()
        {
            var response = new ServiceResponse<List<RoleDTO>>();
            List<RoleDTO> RolesDTOs = new List<RoleDTO>();
            try
            {
                var roles = await _unitOfWork.RoleRepository.GetAllAsync();

                foreach (var role in roles)
                {
                    var roleDto = _mapper.Map<RoleDTO>(role);
                    roleDto.RoleName = role.Name;
                    RolesDTOs.Add(roleDto);
                }
                if (RolesDTOs.Count > 0)
                {
                    response.Data = RolesDTOs;
                    response.Success = true;
                    response.Message = $"Have {RolesDTOs.Count} roles.";
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

        public Task<ServiceResponse<RoleDTO>> GetSRoleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<RoleDTO>>> SearchRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<RoleDTO>> UpdateRoleAsync(int id, RoleUpdateDTO updateDto)
        {
            var reponse = new ServiceResponse<RoleDTO>();

            try
            {
                var enityById = await _unitOfWork.RoleRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updateDto, enityById);
                    var bAfter = _mapper.Map<Role>(newb);
                    _unitOfWork.RoleRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<RoleDTO>(bAfter);
                        reponse.Message = $"Successfull for update Role.";
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
                    reponse.Message = $"Have no Role.";
                    reponse.Error = "Not have a Role";
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
