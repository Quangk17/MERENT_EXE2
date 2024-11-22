using Application.Interfaces;
using Application.Services;
using Application.ViewModels.ProductDTOs;
using Application.ViewModels.RoleDTOs;
using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    public class ProductController: BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ViewAllProduct()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductById(int id)
        {
            // Kiểm tra ID hợp lệ
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            // Gọi service để lấy thông tin sản phẩm
            var result = await _productService.GetProductByIdAsync(id);

            // Kiểm tra nếu không tìm thấy sản phẩm
            if (!result.Success)
            {
                return NotFound(new
                {
                    success = result.Success,
                    message = result.Message
                });
            }

            // Trả về dữ liệu sản phẩm nếu tìm thấy
            return Ok(new
            {
                success = result.Success,
                data = result.Data,
                message = result.Message
            });
        }


        [HttpGet("search-by-name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchProductByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Name parameter cannot be empty.");
            }

            var result = await _productService.SearchProductByNameAsync(name);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }


        //[Authorize (Roles = "Manager")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }
            var c = await _productService.CreateProductAsync(createDto);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        // [Authorize(Roles = "Manager")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO updateDto)
        {
            var c = await _productService.UpdateProductAsync(id, updateDto);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        //  [Authorize(Roles = "Manager, Customer")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var c = await _productService.DeleteProductAsync(id);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }

        [HttpPut("ImgProduct/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImageProduct(int id, [FromBody] UploadImageDTO uploadImage)
        {
            var c = await _productService.UploadImageProductAsync(id, uploadImage);
            if (!c.Success)
            {
                return BadRequest(c);
            }
            return Ok(c);
        }
    }

}

