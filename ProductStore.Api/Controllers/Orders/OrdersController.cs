using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Orders;
using ProductStore.Service.Interfaces.Orders;
using System.Threading.Tasks;

namespace ProductStore.Api.Controllers.Orders
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertAsync([FromBody] OrderForCreationDto dto)
        {
            var result = await _orderService.CreateAsync(dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await _orderService.RetrieveAllAsync(@params);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _orderService.RetrieveByIdAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var result = await _orderService.RemoveAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] OrderForUpdateDto dto)
        {
            var result = await _orderService.ModifyAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
