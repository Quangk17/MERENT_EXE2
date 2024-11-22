using Application.Interfaces;
using Application.ViewModels.ComboProductDTOs;
using Microsoft.AspNetCore.Mvc;

namespace MERENT_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComboOfProductController : ControllerBase
    {
        private readonly IComboOfProductService _service;

        public ComboOfProductController(IComboOfProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetCombosAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("All-Product-ByCombo")]
        public async Task<IActionResult> GetAllByCombo()
        {
            var result = await _service.GetCombosWithProductsDetailsAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("ByCombo/{id:int}")]
        public async Task<IActionResult> GetComboDetailsById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid combo ID.");
            }

            var result = await _service.GetComboWithProductsDetailsByIdAsync(id);

            return result.Success ? Ok(result) : NotFound(result);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetComboByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _service.SearchComboByNameAsync(name);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteComboAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComboOfProductCreateDTO createDto)
        {
            var result = await _service.CreateComboAsync(createDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }

}
