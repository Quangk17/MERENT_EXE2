using Application.ServiceRespones;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.ProductOrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPOService
    {
        Task<ServiceResponse<List<ProductOrderDTO>>> GetProductOrdersAsync();
        Task<ServiceResponse<ProductOrderDTO>> GetProductOrderByIdAsync(int id);
       // Task<ServiceResponse<List<ProductOrderDTO>>> SearchProductOrderByNameAsync(string name);
        Task<ServiceResponse<ProductOrderDTO>> DeleteProductOrderAsync(int id);
        Task<ServiceResponse<ProductOrderDTO>> UpdateProductOrderAsync(int id, ProductOrderUpdateDTO updateDto);
        Task<ServiceResponse<ProductOrderDTO>> CreateProductOrderAsync(ProductOrderCreateDTO productOrder);
        Task<ServiceResponse<List<ProductOrderDTO>>> GetProductOrdersByUserIdAsync(int userId);
        Task<ServiceResponse<ProductOrderDTO>> GetLatestProductOrderByUserIdAsync(int userId);
        Task<ServiceResponse<ProductOrderDTO>> CreateProductOrderForComboAsync(ProductOrderComboCreateDTO productOrderDto);

        Task<ServiceResponse<int>> DeleteInvalidProductOrdersAsync();
        Task<ServiceResponse<int>> FixInvalidStatusOrderAsync();
    }
}
