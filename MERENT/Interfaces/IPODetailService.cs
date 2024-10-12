using Application.ServiceRespones;
using Application.ViewModels.ProductOrderDetailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPODetailService
    {
        Task<ServiceResponse<List<PODetailDTO>>> GetPODetailsAsync();
        Task<ServiceResponse<PODetailDTO>> GetPODetailByIdAsync(int id);
      //  Task<ServiceResponse<List<PODetailDTO>>> SearchPODetailByNameAsync(string name);
        Task<ServiceResponse<PODetailDTO>> DeletePODetailAsync(int id);
        Task<ServiceResponse<PODetailDTO>> UpdatePODetailAsync(int id, PODetailUpdateDTO updateDto);
        Task<ServiceResponse<PODetailDTO>> CreatePODetailAsync(PODetailCreateDTO poDetail);

    }
}
