using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.Orders;

namespace ProductStore.Service.Interfaces.Orders;

public interface IOrderService
{
    Task<bool> RemoveAsync(long id);
    Task<OrderForResultDto> RetrieveByIdAsync(long id);
    Task<OrderForResultDto> CreateAsync(OrderForCreationDto dto);
    Task<OrderForResultDto> ModifyAsync(long id, OrderForUpdateDto dto);
    Task<IEnumerable<OrderForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
