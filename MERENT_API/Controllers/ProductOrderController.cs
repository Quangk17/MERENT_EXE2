using Application.Interfaces;
using Application.Services;
using Application.ViewModels.ProductOrderDTOs;
using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    public class ProductOrderController : BaseController
    {
        private readonly IPOService _productOrderService;

        public ProductOrderController(IPOService productOrderService)
        {
            _productOrderService = productOrderService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllProductOrders()
        {
            var result = await _productOrderService.GetProductOrdersAsync();
            return Ok(result);
        }

       /* [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchProductOrderByName(string name)
        {
            var result = await _productOrderService.SearchProductOrderByNameAsync(name);
            return Ok(result);
        }*/

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProductOrder([FromBody] ProductOrderCreateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var result = await _productOrderService.CreateProductOrderAsync(createDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductOrder(int id, [FromBody] ProductOrderUpdateDTO updateDto)
        {
            var result = await _productOrderService.UpdateProductOrderAsync(id, updateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductOrder(int id)
        {
            var result = await _productOrderService.DeleteProductOrderAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

}
