using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromForm] ProductImageDto dto)
        {
            var result = _productImageService.Add(dto).Result;
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyıd/{id}")]
        public IActionResult GetByCarId(int id)
        {
            var result = _productImageService.GetByImageId(id);
            if (result.IsSuccess)
            {
                return File(result.Data, "image/jpeg");
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _productImageService.GetAll();
            return Ok(result);
        }
    }
}
