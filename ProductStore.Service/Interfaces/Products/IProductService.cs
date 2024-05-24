using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Products;

namespace ProductStore.Service.Interfaces.Products;

public interface IProductService
{
    Task<bool> RemoveAsync(long id);
    Task<ProductForResultDto> SelectByIdAsync(long id);
    Task<IEnumerable<ProductForResultDto>> SelectAllAsync(PaginationParams @params);
    Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto);
    Task<ProductForResultDto> ModifyAsync(long id, ProductForUpdateDto productForUpdateDto);
}
