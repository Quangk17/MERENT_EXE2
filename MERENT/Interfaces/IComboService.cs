using Application.ServiceRespones;
using Application.ViewModels.AccountDTOs;
using Application.ViewModels.ComboDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComboService
    {
        Task<ServiceResponse<List<ComboDTO>>> GetCombosAsync();
        Task<ServiceResponse<ComboDTO>> GetComboByIdAsync(int id);
        Task<ServiceResponse<List<ComboDTO>>> SearchComboByNameAsync(string name);
        Task<ServiceResponse<ComboDTO>> DeleteComboAsync(int id);
        Task<ServiceResponse<ComboDTO>> UpdateComboAsync(int id, ComboUpdateDTOs updateDto);
        Task<ServiceResponse<ComboDTO>> AddComboAsync(ComboCreateDTO combo);
    }
}
