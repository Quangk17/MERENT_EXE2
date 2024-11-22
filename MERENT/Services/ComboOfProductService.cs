using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ComboProductDTOs;
using Application.ViewModels.ProductDTOs;
using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ComboOfProductService : IComboOfProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ComboOfProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ComboOfProductDTO>>> GetCombosAsync()
        {
            var response = new ServiceResponse<List<ComboOfProductDTO>>();
            try
            {
                var entities = await _unitOfWork.ComboOfProductRepository.GetAllAsync();
                response.Data = entities.Select(e => _mapper.Map<ComboOfProductDTO>(e)).ToList();
                response.Success = true;
                response.Message = "Fetched all ComboOfProducts successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ComboOfProductDTO>> GetComboByIdAsync(int id)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                response.Success = true;
                response.Message = "Fetched ComboOfProduct successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ComboOfProductDTO>>> SearchComboByNameAsync(string name)
        {
            var response = new ServiceResponse<List<ComboOfProductDTO>>();
            try
            {
                var entities = await _unitOfWork.ComboOfProductRepository.GetAllAsync(x => x.Combo.Name.Contains(name));
                response.Data = entities.Select(e => _mapper.Map<ComboOfProductDTO>(e)).ToList();
                response.Success = true;
                response.Message = $"Found {response.Data.Count} ComboOfProducts matching '{name}'.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ComboOfProductDTO>> DeleteComboAsync(int id)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                _unitOfWork.ComboOfProductRepository.SoftRemove(entity);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Deleted ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to delete ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        /*public async Task<ServiceResponse<ComboOfProductDTO>> UpdateComboAsync(int id1, int id2, ComboOfProductUpdateDTO updateDto)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = await _unitOfWork.ComboOfProductRepository.GetByIdAsync(new { ComboID = id1, ProductID = id2 });
                if (entity == null)
                {
                    response.Success = false;
                    response.Message = "ComboOfProduct not found.";
                    return response;
                }

                _mapper.Map(updateDto, entity);
                _unitOfWork.ComboOfProductRepository.Update(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Updated ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to update ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }*/

        public async Task<ServiceResponse<ComboOfProductDTO>> CreateComboAsync(ComboOfProductCreateDTO createDto)
        {
            var response = new ServiceResponse<ComboOfProductDTO>();
            try
            {
                var entity = _mapper.Map<ComboOfProduct>(createDto);
                await _unitOfWork.ComboOfProductRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ComboOfProductDTO>(entity);
                    response.Success = true;
                    response.Message = "Created ComboOfProduct successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to create ComboOfProduct.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ComboWithProductsDTO>>> GetCombosWithProductsAsync()
        {
            var response = new ServiceResponse<List<ComboWithProductsDTO>>();

            try
            {
                var combos = await _unitOfWork.ComboRepository.GetAllAsync(c => c.ComboOfProducts);

                var result = combos.Select(combo => new ComboWithProductsDTO
                {
                    ComboID = combo.Id,
                    ProductIDs = combo.ComboOfProducts.Select(cop => cop.ProductID).ToList()
                }).ToList();

                response.Data = result;
                response.Success = true;
                response.Message = "Fetched all combos with products successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<List<ComboDetailsDTO>>> GetCombosWithProductsDetailsAsync()
        {
            var response = new ServiceResponse<List<ComboDetailsDTO>>();

            try
            {
                // Lấy tất cả combo bao gồm các product bên trong
                var combos = await _unitOfWork.ComboOfProductRepository.GetAllCombosWithProductsAsync();

                var result = combos.Select(combo =>
                {
                    var totalPrice = combo.ComboOfProducts != null
                        ? combo.ComboOfProducts
                            .Where(cop => cop.Product != null)
                            .Sum(cop => (cop.Product.Price ?? 0) * cop.Quantity)
                        : 0;

                    return new ComboDetailsDTO
                    {
                        ComboID = combo.Id,
                        ComboName = combo.Name,
                        Description = combo.Description,
                        UrlImg = combo.UrlImg,
                        Products = combo.ComboOfProducts != null
                            ? combo.ComboOfProducts
                                .Where(cop => cop.Product != null)
                                .Select(cop => new ProductComboDTO
                                {
                                    Id = cop.Product.Id,
                                    Name = cop.Product.Name,
                                    Description = cop.Product.Description,
                                    ProductType = cop.Product.ProductType,
                                    Price = cop.Product.Price,
                                    Quantity = cop.Quantity, // Thêm số lượng vào đây
                                    URLCenter = cop.Product.URLCenter,
                                    URLLeft = cop.Product.URLLeft,
                                    URLRight = cop.Product.URLRight,
                                    URLSide = cop.Product.URLSide
                                }).ToList()
                            : new List<ProductComboDTO>(),
                        TotalPrice = totalPrice
                    };
                }).ToList();

                response.Data = result;
                response.Success = true;
                response.Message = "Fetched all combos with product details successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }



        public async Task<ServiceResponse<ComboDetailsDTO>> GetComboWithProductsDetailsByIdAsync(int comboId)
        {
            var response = new ServiceResponse<ComboDetailsDTO>();

            try
            {
                // Lấy combo theo ID bao gồm các product bên trong
                var combo = await _unitOfWork.ComboOfProductRepository.GetComboWithProductsByIdAsync(comboId);

                if (combo == null)
                {
                    response.Success = false;
                    response.Message = "Combo not found.";
                    return response;
                }

                var totalPrice = combo.ComboOfProducts != null
                    ? combo.ComboOfProducts
                        .Where(cop => cop.Product != null)
                        .Sum(cop => (cop.Product.Price ?? 0) * cop.Quantity)
                    : 0;

                // Mapping dữ liệu sang DTO
                var comboDetails = new ComboDetailsDTO
                {
                    ComboID = combo.Id,
                    ComboName = combo.Name,
                    Description = combo.Description,
                    UrlImg = combo.UrlImg,
                    Products = combo.ComboOfProducts != null
                        ? combo.ComboOfProducts
                            .Where(cop => cop.Product != null)
                            .Select(cop => new ProductComboDTO
                            {
                                Id = cop.Product.Id,
                                Name = cop.Product.Name,
                                Description = cop.Product.Description,
                                ProductType = cop.Product.ProductType,
                                Price = cop.Product.Price,
                                Quantity = cop.Quantity, // Thêm số lượng vào đây
                                URLCenter = cop.Product.URLCenter,
                                URLLeft = cop.Product.URLLeft,
                                URLRight = cop.Product.URLRight,
                                URLSide = cop.Product.URLSide
                            }).ToList()
                        : new List<ProductComboDTO>(),
                    TotalPrice = totalPrice
                };

                response.Data = comboDetails;
                response.Success = true;
                response.Message = "Fetched combo with product details successfully.";
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
