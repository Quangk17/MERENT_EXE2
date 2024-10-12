using Application.Interfaces;
using Application.ViewModels.ProductOrderDetailDTOs;
using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    public class ProductOrderDetailController : BaseController
    {
        private readonly IPODetailService _productOrderDetailService;

        public ProductOrderDetailController(IPODetailService productOrderDetailService)
        {
            _productOrderDetailService = productOrderDetailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllProductOrderDetails()
        {
            var result = await _productOrderDetailService.GetPODetailsAsync();
            return Ok(result);
        }

        /* [HttpGet("{name}")]
         [ProducesResponseType(StatusCodes.Status200OK)]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
         public async Task<IActionResult> SearchProductOrderDetailByName(string name)
         {
             var result = await _productOrderDetailService.SearchProductOrderDetailByNameAsync(name);
             return Ok(result);
         } */

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProductOrderDetail([FromBody] PODetailCreateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var result = await _productOrderDetailService.CreatePODetailAsync(createDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProductOrderDetail(int id, [FromBody] PODetailUpdateDTO updateDto)
        {
            var result = await _productOrderDetailService.UpdatePODetailAsync(id, updateDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProductOrderDetail(int id)
        {
            var result = await _productOrderDetailService.DeletePODetailAsync(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }

}
