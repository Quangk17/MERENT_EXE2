using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.RoleDTOs;
using AutoMapper;
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

        public Task<ServiceResponse<RoleDTO>> CreateRoleAsync(RoleCreateDTO role)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<RoleDTO>> DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<RoleDTO>>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<RoleDTO>> GetSRoleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<RoleDTO>>> SearchRoleByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<RoleDTO>> UpdateRoleAsync(int id, RoleUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
