using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ProductDTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<ProductDTO>> CreateProductAsync(ProductCreateDTO product)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ProductDTO>> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<ProductDTO>>> SearchProductByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<ProductDTO>> UpdateProdcutAsync(int id, ProductUpdateDTO updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
