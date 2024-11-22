using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ProductOrderDTOs;
using AutoMapper;
using Domain.Entites;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductOrderService : IPOService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductOrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ProductOrderDTO>> CreateProductOrderAsync(ProductOrderCreateDTO productOrder)
        {
            var response = new ServiceResponse<ProductOrderDTO>();

            try
            {
                var entity = _mapper.Map<ProductOrder>(productOrder);

                await _unitOfWork.ProductOrderRepository.AddAsync(entity);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ProductOrderDTO>(entity);
                    response.Success = true;
                    response.Message = "Create new ProductOrder successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Create new ProductOrder failed";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ProductOrderDTO>> DeleteProductOrderAsync(int id)
        {
            var response = new ServiceResponse<ProductOrderDTO>();
            var order = await _unitOfWork.ProductOrderRepository.GetByIdAsync(id);

            if (order != null)
            {
                _unitOfWork.ProductOrderRepository.SoftRemove(order);

                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    response.Data = _mapper.Map<ProductOrderDTO>(order);
                    response.Success = true;
                    response.Message = "Deleted ProductOrder successfully!";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Failed to delete ProductOrder!";
                }
            }
            else
            {
                response.Success = false;
                response.Message = "ProductOrder not found";
            }

            return response;
        }

        public async Task<ServiceResponse<ProductOrderDTO>> GetProductOrderByIdAsync(int id)
        {
            var response = new ServiceResponse<ProductOrderDTO>();
            var order = await _unitOfWork.ProductOrderRepository.GetByIdAsync(id);

            if (order != null)
            {
                response.Data = _mapper.Map<ProductOrderDTO>(order);
                response.Success = true;
                response.Message = "ProductOrder found";
            }
            else
            {
                response.Success = false;
                response.Message = "ProductOrder not found";
            }

            return response;
        }

        public async Task<ServiceResponse<List<ProductOrderDTO>>> GetProductOrdersAsync()
        {
            var response = new ServiceResponse<List<ProductOrderDTO>>();
            var orders = await _unitOfWork.ProductOrderRepository.GetAllAsync();

            // Debug thử danh sách orders để kiểm tra giá trị của StatusOrder
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, StatusOrder: {order.StatusOrder}");
            }

            var orderDTOs = orders.Select(order => _mapper.Map<ProductOrderDTO>(order)).ToList();

            if (orderDTOs.Any())
            {
                response.Data = orderDTOs;
                response.Success = true;
                response.Message = $"Found {orderDTOs.Count} product orders.";
            }
            else
            {
                response.Success = false;
                response.Message = "No product orders found.";
            }

            return response;
        }


        /* public async Task<ServiceResponse<List<ProductOrderDTO>>> SearchProductOrderByNameAsync(string name)
         {
             var response = new ServiceResponse<List<ProductOrderDTO>>();
             var orders = await _unitOfWork.ProductOrderRepository.SearchByNameAsync(name);
             var orderDTOs = orders.Select(order => _mapper.Map<ProductOrderDTO>(order)).ToList();

             if (orderDTOs.Any())
             {
                 response.Data = orderDTOs;
                 response.Success = true;
                 response.Message = $"Found {orderDTOs.Count} product orders matching '{name}'.";
             }
             else
             {
                 response.Success = false;
                 response.Message = $"No product orders found matching '{name}'.";
             }

             return response;
         }*/

        public async Task<ServiceResponse<ProductOrderDTO>> UpdateProductOrderAsync(int id, ProductOrderUpdateDTO updateDto)
        {
            var response = new ServiceResponse<ProductOrderDTO>();

            try
            {
                var entityById = await _unitOfWork.ProductOrderRepository.GetByIdAsync(id);

                if (entityById != null)
                {
                    var updatedEntity = _mapper.Map(updateDto, entityById);
                    _unitOfWork.ProductOrderRepository.Update(updatedEntity);

                    if (await _unitOfWork.SaveChangeAsync() > 0)
                    {
                        response.Success = true;
                        response.Data = _mapper.Map<ProductOrderDTO>(updatedEntity);
                        response.Message = "ProductOrder updated successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Failed to update ProductOrder";
                    }
                }
                else
                {
                    response.Success = false;
                    response.Message = "ProductOrder not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }


        public async Task<ServiceResponse<List<ProductOrderDTO>>> GetProductOrdersByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<List<ProductOrderDTO>>();

            try
            {
                var orders = await _unitOfWork.ProductOrderRepository.GetProductOrdersByUserIdAsync(userId);
                var orderDTOs = orders.Select(order => _mapper.Map<ProductOrderDTO>(order)).ToList();

                if (orderDTOs.Any())
                {
                    response.Data = orderDTOs;
                    response.Success = true;
                    response.Message = $"Found {orderDTOs.Count} product orders for user {userId}.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No product orders found for this user.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ProductOrderDTO>> GetLatestProductOrderByUserIdAsync(int userId)
        {
            var response = new ServiceResponse<ProductOrderDTO>();

            try
            {
                var latestOrder = await _unitOfWork.ProductOrderRepository.GetLatestProductOrderByUserIdAsync(userId);

                if (latestOrder != null)
                {
                    response.Data = _mapper.Map<ProductOrderDTO>(latestOrder);
                    response.Success = true;
                    response.Message = "Successfully retrieved the latest product order.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "No product orders found for this user.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<ProductOrderDTO>> CreateProductOrderForComboAsync(ProductOrderComboCreateDTO productOrderDto)
        {
            var response = new ServiceResponse<ProductOrderDTO>();

            try
            {
                // Tạo ProductOrder
                var productOrder = _mapper.Map<ProductOrder>(productOrderDto);

                // Gán giá trị mặc định cho StatusOrder
                productOrder.StatusOrder = OrderStatusTypeEnums.Pending.ToString();

                // Lưu ProductOrder vào DB trước để lấy Id
                await _unitOfWork.ProductOrderRepository.AddAsync(productOrder);
                await _unitOfWork.SaveChangeAsync();

                // Danh sách ProductOrderDetails
                var productOrderDetails = new List<ProductOrderDetails>();
                long totalAmount = 0;
                long totalPrice = 0;

                // Danh sách tất cả ProductID từ các Combo
                var productIds = new List<int>();

                foreach (var comboId in productOrderDto.ComboIds)
                {
                    var combo = await _unitOfWork.ComboRepository.GetByIdAsync(comboId, x => x.ComboOfProducts);

                    if (combo == null || combo.ComboOfProducts == null)
                    {
                        response.Success = false;
                        response.Message = $"Combo with ID {comboId} not found or has no products.";
                        return response;
                    }

                    // Thêm tất cả ProductID từ Combo vào danh sách
                    productIds.AddRange(combo.ComboOfProducts.Select(cop => cop.ProductID));
                }

                // Lấy thông tin tất cả sản phẩm từ ProductRepository
                var products = await _unitOfWork.ProductRepository.GetAllProductsByIdsAsync(productIds);

                // Tạo ProductOrderDetails
                foreach (var comboId in productOrderDto.ComboIds)
                {
                    var combo = await _unitOfWork.ComboRepository.GetByIdAsync(comboId, x => x.ComboOfProducts);

                    foreach (var comboProduct in combo.ComboOfProducts)
                    {
                        var product = products.FirstOrDefault(p => p.Id == comboProduct.ProductID);
                        if (product == null) continue;

                        var unitPrice = product.Price ?? 0;
                        var quantity = comboProduct.Quantity;

                        productOrderDetails.Add(new ProductOrderDetails
                        {
                            OrderId = productOrder.Id,
                            ProductID = comboProduct.ProductID,
                            Quantity = quantity,
                            UnitPrice = unitPrice
                        });

                        // Tính tổng số lượng và tổng giá
                        totalAmount += quantity;
                        totalPrice += quantity * unitPrice;
                    }
                }

                // Gán tổng số lượng và giá trị cho ProductOrder
                productOrder.TotalAmount = totalAmount;
                productOrder.TotalPrice = totalPrice;

                // Cập nhật ProductOrder với thông tin tổng số lượng và giá
                _unitOfWork.ProductOrderRepository.Update(productOrder);

                // Lưu ProductOrderDetails vào DB
                await _unitOfWork.PODetailRepository.AddRangeAsync(productOrderDetails);
                await _unitOfWork.SaveChangeAsync();

                response.Data = _mapper.Map<ProductOrderDTO>(productOrder);
                response.Success = true;
                response.Message = "Product order created successfully with details.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }

        public async Task<ServiceResponse<int>> DeleteInvalidProductOrdersAsync()
        {
            var response = new ServiceResponse<int>();

            try
            {
                // Lấy tất cả ProductOrder có StatusOrder không hợp lệ
                var invalidOrders = await _unitOfWork.ProductOrderRepository.GetAllAsync();

                var ordersToDelete = invalidOrders
                    .Where(order => !Enum.TryParse<OrderStatusTypeEnums>(order.StatusOrder, true, out _))
                    .ToList();

                if (ordersToDelete.Any())
                {
                    // Xóa các ProductOrder bị lỗi
                    _unitOfWork.ProductOrderRepository.SoftRemoveRange(ordersToDelete);
                    var changes = await _unitOfWork.SaveChangeAsync();

                    response.Data = changes;
                    response.Success = true;
                    response.Message = $"{ordersToDelete.Count} invalid ProductOrder(s) deleted.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "No invalid ProductOrder(s) found.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessages = new List<string> { ex.Message };
            }

            return response;
        }
        public async Task<ServiceResponse<int>> FixInvalidStatusOrderAsync()
        {
            var response = new ServiceResponse<int>();

            try
            {
                // Lấy tất cả ProductOrder mà không kiểm tra IsDeleted
                var orders = await _unitOfWork.ProductOrderRepository.GetAllAsync();

                // Lọc các ProductOrder có StatusOrder không hợp lệ
                var invalidOrders = orders.Where(order => !Enum.TryParse<OrderStatusTypeEnums>(order.StatusOrder, true, out _)).ToList();

                if (invalidOrders.Any())
                {
                    // Gán giá trị mặc định cho các bản ghi lỗi
                    foreach (var order in invalidOrders)
                    {
                        order.StatusOrder = OrderStatusTypeEnums.Pending.ToString();
                    }

                    // Cập nhật lại DB
                    _unitOfWork.ProductOrderRepository.UpdateRange(invalidOrders);
                    var changes = await _unitOfWork.SaveChangeAsync();

                    response.Data = changes;
                    response.Success = true;
                    response.Message = $"{invalidOrders.Count} invalid ProductOrder(s) fixed.";
                }
                else
                {
                    response.Success = true;
                    response.Message = "No invalid ProductOrder(s) found.";
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
