using System.ComponentModel.Design;
using Business.Abstract;
using Core.Utilities.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly string _imageRoot = "/api/ProductImage/getbyıd/";

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var result = _productService.GetAll(filter, route);
            return Task.FromResult<IActionResult>(Ok(result));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductPostDto menu, [FromForm] List<IFormFile> files)
        {
           var result = await _productService.Add(menu, files);
           return Ok(result);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =  await _productService.GetProductDetailById(id, _imageRoot);
            return Ok(result);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetAllDetails([FromQuery] PaginationFilter filter)
        {
            var result = await _productService.GetAllWithDetails(filter, _imageRoot);
            return Ok(result);
        }
        [HttpGet("DetailsByCategory/{categoryId}")]
        public async Task<IActionResult> GetAllDetailsByCategoryId([FromQuery] PaginationFilter filter, int categoryId)
        {
            var result = await _productService.GetByCategoryId(filter, _imageRoot, categoryId);
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProductPostDto product, int id)
        {
            var result = await _productService.Update(product, id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize("Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
