using Microsoft.AspNetCore.Mvc;
using ProductStore.Api.Controllers.Commons;
using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Categories;
using ProductStore.Service.DTOs.CategoryDTOs;
using ProductStore.Service.Interfaces.Categories;
using ProductStore.Api.Helpers;

namespace ProductStore.Api.Controllers.Categories
{
    public class CategoriesController : BaseController
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] CategoryForCreationDto dto)
           => Ok(new Response
           {
               Code = 200,
               Message = "Success",
               Data = await _categoryService.CreateAsync(dto)
           });

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _categoryService.RetrieveAllAsync(@params)
            });

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute(Name = "id")] long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _categoryService.RetrieveByIdAsync(id)
            });

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")] long id, [FromForm] CategoryForUpdateDto dto)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _categoryService.ModifyAsync(id, dto)
            });

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
            => Ok(new Response
            {
                Code = 200,
                Message = "Success",
                Data = await _categoryService.ReamoveAsync(id)
            });
    }
}
