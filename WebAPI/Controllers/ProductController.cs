using System.ComponentModel.Design;
using Business.Abstract;
using Core.Utilities.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

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
        public Task<IActionResult> Add([FromBody] Product menu )
        {
           var result = _productService.Add(menu);
           return Task.FromResult<IActionResult>(Ok(result));
        }
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetAll(int id)
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
    }
}
