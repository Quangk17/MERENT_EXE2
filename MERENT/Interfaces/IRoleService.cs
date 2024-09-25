using Application.ServiceRespones;
using Application.ViewModels.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<ServiceResponse<List<RoleDTO>>> GetRolesAsync();
        Task<ServiceResponse<RoleDTO>> GetSRoleByIdAsync(int id);
        Task<ServiceResponse<List<RoleDTO>>> SearchRoleByNameAsync(string name);
        Task<ServiceResponse<RoleDTO>> DeleteRoleAsync(int id);
        Task<ServiceResponse<RoleDTO>> UpdateRoleAsync(int id, RoleUpdateDTO updateDto);
        Task<ServiceResponse<RoleDTO>> CreateRoleAsync(RoleCreateDTO role);
    }
}
