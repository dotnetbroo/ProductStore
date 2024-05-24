using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Products;
using ProductStore.Service.Interfaces.Products;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> AddAsync([FromForm] ProductForCreationDto productForCreationDto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _productService.CreateAsync(productForCreationDto)
            });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _productService.SelectAllAsync(@params)
            });


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([Required] long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _productService.SelectByIdAsync(id)
            });

        [HttpPut]
        public async Task<IActionResult> ModifyAsync(long id, [FromForm] ProductForUpdateDto productForUpdate)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _productService.ModifyAsync(id, productForUpdate)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([Required] long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _productService.RemoveAsync(id)
            });

    }
}
