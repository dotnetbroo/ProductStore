using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Products;
using ProductStore.Service.Interfaces.Products;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ProductStore.Api.Controllers.Products
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddAsync([FromBody] ProductForCreationDto productForCreationDto)
        {
            var result = await _productService.CreateAsync(productForCreationDto);
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
            var result = await _productService.SelectAllAsync(@params);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync([Required] long id)
        {
            var result = await _productService.SelectByIdAsync(id);
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
        public async Task<IActionResult> ModifyAsync([FromRoute] long id, [FromBody] ProductForUpdateDto productForUpdate)
        {
            var result = await _productService.ModifyAsync(id, productForUpdate);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([Required] long id)
        {
            var result = await _productService.RemoveAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
