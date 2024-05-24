using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Categories;
using ProductStore.Service.DTOs.CategoryDTOs;
using ProductStore.Service.DTOs.Products;

namespace ProductStore.Service.Interfaces.Categories;

public interface ICategoryService
{
    Task<bool> ReamoveAsync(long id);
    Task<CategoryForResultDto> RetrieveByIdAsync(long id);
    Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto);
    Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto);
    Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
