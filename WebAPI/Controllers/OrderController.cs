using System.Reflection.Metadata.Ecma335;
using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
        {
            var result =  await _orderService.CreateOrder(dto);
            return Ok(result);

        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }
        [HttpPost("GetWithCustomerId/{id}")]
        public async Task<IActionResult> GetByCustomerId(int id)
        {
            var result = await _orderService.GetOrdersByCustomerId(id);
            return Ok(result);
        }
        [HttpPost("GetOrderDetailById/{id}")]
        public async Task<IActionResult> GetOrderDetailById(int id)
        {
            var result = await _orderService.GetOrderDetailsById(id);
            return Ok(result);
        }
    }
}
