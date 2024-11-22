using Application.ServiceRespones;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.RoleDTOs;


namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync();
        Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id);
        Task<ServiceResponse<List<ProductDTO>>> SearchProductByNameAsync(string name);
        Task<ServiceResponse<ProductDTO>> DeleteProductAsync(int id);
        Task<ServiceResponse<ProductDTO>> UpdateProductAsync(int id, ProductUpdateDTO updateDto);
        Task<ServiceResponse<ProductDTO>> CreateProductAsync(ProductCreateDTO product);
        Task<ServiceResponse<ProductDTO>> UploadImageProductAsync(int id, UploadImageDTO uploadImageDTO);
        Task<ServiceResponse<List<ProductDTO>>> GetLatestProductsAsync(int count = 4);
        Task<ServiceResponse<List<ProductDTO>>> GetMostRentedProductsAsync(int count = 4);
    }
}
