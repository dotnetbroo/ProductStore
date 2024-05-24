using ProductStore.Domain.Entities.Users;
using ProductStore.Domain.Enums;

namespace ProductStore.Service.DTOs.Orders;

public record OrderForUpdateDto
{
    public long UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
}
