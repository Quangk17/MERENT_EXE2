using Application.ServiceRespones;
using Application.ViewModels.ServiceDTOs;
using Application.ViewModels.StoreDTOs;


namespace Application.Interfaces
{
    public interface IServiceService
    {
        Task<ServiceResponse<List<ServiceDTO>>> GetServicesAsync();
        Task<ServiceResponse<ServiceDTO>> GetServiceByIdAsync(int id);
        Task<ServiceResponse<List<ServiceDTO>>> SearchServiceByNameAsync(string name);
        Task<ServiceResponse<ServiceDTO>> DeleteServiceAsync(int id);
        Task<ServiceResponse<ServiceDTO>> UpdateServiceAsync(int id, ServiceUpdateDTO updateDto);
        Task<ServiceResponse<ServiceDTO>> CreateServiceAsync(ServiceCreateDTO store);
    }
}
