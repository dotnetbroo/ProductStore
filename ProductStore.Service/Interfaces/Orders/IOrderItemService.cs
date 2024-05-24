using ProductStore.Domain.Configurations;
using ProductStore.Service.DTOs.OrderItems;

namespace ProductStore.Service.Interfaces.Orders;

public interface IOrderItemService
{
    Task<bool> RemoveAsync(long id);
    Task<OrderItemForResultDto> RetrieveByIdAsync(long id);
    Task<OrderItemForResultDto> CreateAsync(OrderItemForCreationDto dto);
    Task<OrderItemForResultDto> ModifyAsync(long id, OrderItemForUpdateDto dto);
    Task<IEnumerable<OrderItemForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
