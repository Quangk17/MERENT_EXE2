using Application.Interfaces;
using Application.ServiceRespones;
using Application.ViewModels.ProductOrderDTOs;
using AutoMapper;
using Domain.Entites;
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

    }
}
