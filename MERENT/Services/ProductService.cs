using Application.Interfaces;
using Application.Repositories;
using Application.ServiceRespones;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.ServiceDTOs;
using AutoMapper;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
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

        public async Task<ServiceResponse<ProductDTO>> CreateProductAsync(ProductCreateDTO createdto)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var entity = _mapper.Map<Product>(createdto);

                await _unitOfWork.ProductRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    reponse.Data = _mapper.Map<ProductDTO>(entity);
                    reponse.Success = true;
                    reponse.Message = "Create new Court successfully";
                    return reponse;
                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = "Create new Court fail";
                    return reponse;
                }
            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }

            return reponse;
        }

        public async Task<ServiceResponse<ProductDTO>> DeleteProductAsync(int id)
        {
            var _response = new ServiceResponse<ProductDTO>();
            var court = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            if (court != null)
            {
                _unitOfWork.ProductRepository.SoftRemove(court);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    _response.Data = _mapper.Map<ProductDTO>(court);
                    _response.Success = true;
                    _response.Message = "Deleted Court Successfully!";
                }
                else
                {
                    _response.Success = false;
                    _response.Message = "Deleted Court Fail!";
                }
            }
            else
            {
                _response.Success = false;
                _response.Message = "Court not found";
            }

            return _response;
        }

        public async Task<ServiceResponse<ProductDTO>> GetProductByIdAsync(int id)
        {
            var response = new ServiceResponse<ProductDTO>();

            try
            {
                // Truy vấn sản phẩm từ cơ sở dữ liệu
                var product = await _unitOfWork.ProductRepository.Query()
                    .FirstOrDefaultAsync(p => p.Id == id);

                // Kiểm tra nếu không tìm thấy sản phẩm
                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    return response;
                }

                // Mapping từ Product Entity sang ProductDTO
                response.Data = _mapper.Map<ProductDTO>(product);
                response.Success = true;
                response.Message = "Product retrieved successfully.";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<List<ProductDTO>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<ProductDTO>>();
            List<ProductDTO> CourtDTOs = new List<ProductDTO>();
            try
            {
                var courts = await _unitOfWork.ProductRepository.GetAllAsync();

                foreach (var court in courts)
                {
                    var courtDto = _mapper.Map<ProductDTO>(court);
                    courtDto.Name = court.Name;
                    CourtDTOs.Add(courtDto);
                }
                if (CourtDTOs.Count > 0)
                {
                    response.Data = CourtDTOs;
                    response.Success = true;
                    response.Message = $"Have {CourtDTOs.Count} roles.";
                    response.Error = "No error";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No roles found.";
                    response.Error = "No roles available";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = "Exception";
                response.ErrorMessages = new List<string> { ex.Message };
            }
            return response;

        }

        public async Task<ServiceResponse<List<ProductDTO>>> SearchProductByNameAsync(string name)
        {
            var response = new ServiceResponse<List<ProductDTO>>();
            try
            {
                var products = await _unitOfWork.ProductRepository.Query()
                    .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(name.ToLower()))
                    .ToListAsync();

                response.Data = _mapper.Map<List<ProductDTO>>(products);
                response.Success = true;
                response.Message = "Products retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<ProductDTO>> UpdateProductAsync(int id, ProductUpdateDTO updatedto)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var enityById = await _unitOfWork.ProductRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(updatedto, enityById);
                    var bAfter = _mapper.Map<Product>(newb);
                    _unitOfWork.ProductRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ProductDTO>(bAfter);
                        reponse.Message = $"Successfull for update Court.";
                        return reponse;
                    }
                    else
                    {
                        reponse.Success = false;
                        reponse.Error = "Save update failed";
                        return reponse;
                    }

                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = $"Have no Court.";
                    reponse.Error = "Not have a Court";
                    return reponse;
                }

            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }

            return reponse;
        }

        public async Task<ServiceResponse<ProductDTO>> UploadImageProductAsync(int id, UploadImageDTO uploadImageDTO)
        {
            var reponse = new ServiceResponse<ProductDTO>();

            try
            {
                var enityById = await _unitOfWork.ProductRepository.GetByIdAsync(id);

                if (enityById != null)
                {
                    var newb = _mapper.Map(uploadImageDTO, enityById);
                    var bAfter = _mapper.Map<Product>(newb);
                    _unitOfWork.ProductRepository.Update(bAfter);
                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        reponse.Success = true;
                        reponse.Data = _mapper.Map<ProductDTO>(bAfter);
                        reponse.Message = $"Successfull for update Product.";
                        return reponse;
                    }
                    else
                    {
                        reponse.Success = false;
                        reponse.Error = "Save update failed";
                        return reponse;
                    }

                }
                else
                {
                    reponse.Success = false;
                    reponse.Message = $"Have no Product.";
                    reponse.Error = "Not have a Product";
                    return reponse;
                }

            }
            catch (Exception ex)
            {
                reponse.Success = false;
                reponse.ErrorMessages = new List<string> { ex.Message };
                return reponse;
            }

            return reponse;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetLatestProductsAsync(int count = 4)
        {
            var response = new ServiceResponse<List<ProductDTO>>();

            try
            {
                // Lấy danh sách sản phẩm theo ngày tạo, giới hạn `count` sản phẩm
                var products = await _unitOfWork.ProductRepository.GetAllAsync();
                var latestProducts = products
                    .OrderByDescending(product => product.CreationDate) // Sắp xếp theo ngày tạo giảm dần
                    .Take(count) // Lấy tối đa `count` sản phẩm
                    .ToList();

                // Ánh xạ sang DTO
                var productDTOs = latestProducts.Select(product => _mapper.Map<ProductDTO>(product)).ToList();

                if (productDTOs.Any())
                {
                    response.Data = productDTOs;
                    response.Success = true;
                    response.Message = $"Found {productDTOs.Count} latest products.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No products found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProductDTO>>> GetMostRentedProductsAsync(int count = 4)
        {
            var response = new ServiceResponse<List<ProductDTO>>();

            try
            {
                // Lấy tất cả ProductOrderDetails
                var orderDetails = await _unitOfWork.PODetailRepository.GetAllAsync();

                // Đếm số lần xuất hiện của từng ProductID
                var productCounts = orderDetails
                    .GroupBy(detail => detail.ProductID)
                    .Select(group => new
                    {
                        ProductID = group.Key,
                        RentCount = group.Sum(detail => detail.Quantity ?? 0) // Tổng số lượng thuê
                    })
                    .OrderByDescending(x => x.RentCount) // Sắp xếp giảm dần theo số lần thuê
                    .Take(count) // Lấy tối đa `count` sản phẩm
                    .ToList();

                // Lấy danh sách ProductID, loại bỏ null
                var productIds = productCounts
                    .Select(x => x.ProductID)
                    .Where(id => id.HasValue) // Loại bỏ null
                    .Select(id => id.Value)  // Chuyển từ int? sang int
                    .ToList();

                // Lấy thông tin chi tiết của các sản phẩm
                var products = await _unitOfWork.ProductRepository.GetAllProductsByIdsAsync(productIds);

                // Ánh xạ sang DTO
                var productDTOs = products.Select(product => _mapper.Map<ProductDTO>(product)).ToList();

                if (productDTOs.Any())
                {
                    response.Data = productDTOs;
                    response.Success = true;
                    response.Message = $"Found {productDTOs.Count} most rented products.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No products found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

    }
}
