using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CategoryDto category)
        {
            var result =  await _categoryService.Add(category);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public Task<IActionResult> Update([FromBody] CategoryDto category, int id)
        {
            var result = _categoryService.Update(category, id);
            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.Delete(id);
            return Ok(result);
        }

    }
}
