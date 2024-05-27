using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Api.Helpers;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Categories;
using ProductStore.Service.DTOs.CategoryDTOs;
using ProductStore.Service.Interfaces.Categories;

namespace ProductStore.Api.Controllers.Categories
{
    //[Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[Authorize(Policy = "Admins")]
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PostAsync([FromBody] CategoryForCreationDto dto)
        {
            var result = await _categoryService.CreateAsync(dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        //[Authorize("Admins")]
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            var result = await _categoryService.RetrieveAllAsync(@params);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

        //[Authorize("Admins")]
        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _categoryService.RetrieveByIdAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

       // [Authorize("Admins")]
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromBody] CategoryForUpdateDto dto)
        {
            var result = await _categoryService.ModifyAsync(id, dto);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }

       // [Authorize("Admins")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        {
            var result = await _categoryService.ReamoveAsync(id);
            return Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = result
            });
        }
    }
}
