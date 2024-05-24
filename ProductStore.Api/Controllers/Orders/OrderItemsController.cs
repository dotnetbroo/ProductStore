using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.OrderItems;
using ProductStore.Service.Interfaces.Orders;
using System.Threading.Tasks;

namespace ProductStore.Api.Controllers.Orders
{
    public class OrderItemsController : BaseController
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> InsertAsync([FromBody] OrderItemForCreationDto dto)
        {
            var result = await _orderItemService.CreateAsync(dto);
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
            var result = await _orderItemService.RetrieveAllAsync(@params);
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
            var result = await _orderItemService.RetrieveByIdAsync(id);
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
            var result = await _orderItemService.RemoveAsync(id);
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
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] OrderItemForUpdateDto dto)
        {
            var result = await _orderItemService.ModifyAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
