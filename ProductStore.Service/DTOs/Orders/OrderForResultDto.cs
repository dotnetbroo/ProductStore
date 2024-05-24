using ProductStore.Domain.Entities.Users;
using ProductStore.Domain.Enums;
using ProductStore.Service.DTOs.OrderItems;

namespace ProductStore.Service.DTOs.Orders;

public record OrderForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public ICollection<OrderItemForResultDto> OrderItems { get; set; }
}
