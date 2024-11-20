using Application.ServiceRespones;
using Application.ViewModels.ComboDTOs;
using Application.ViewModels.ComboProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IComboOfProductService
    {
        Task<ServiceResponse<List<ComboOfProductDTO>>> GetCombosAsync();
        Task<ServiceResponse<ComboOfProductDTO>> GetComboByIdAsync(int id);
        Task<ServiceResponse<List<ComboOfProductDTO>>> SearchComboByNameAsync(string name);
        Task<ServiceResponse<ComboOfProductDTO>> DeleteComboAsync(int id);
        //Task<ServiceResponse<ComboDTO>> UpdateComboAsync(int id1, int id2 ,ComboOfProductUpdateDTO updateDto);
        Task<ServiceResponse<ComboOfProductDTO>> CreateComboAsync(ComboOfProductCreateDTO combo);
        Task<ServiceResponse<List<ComboWithProductsDTO>>> GetCombosWithProductsAsync();
    }
}
