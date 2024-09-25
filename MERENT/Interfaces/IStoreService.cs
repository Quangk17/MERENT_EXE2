using Application.ServiceRespones;
using Application.ViewModels.StoreDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStoreService
    {
        Task<ServiceResponse<List<StoreDTO>>> GetStoresAsync();
        Task<ServiceResponse<StoreDTO>> GetStoreByIdAsync(int id);
        Task<ServiceResponse<List<StoreDTO>>> SearchStoreByNameAsync(string name);
        Task<ServiceResponse<StoreDTO>> DeleteStoreAsync(int id);
        Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(int id, StoreUpdateDTO updateDto);
        Task<ServiceResponse<StoreDTO>> CreateStoreAsync(StoreCreateDTO store);
    }
}
