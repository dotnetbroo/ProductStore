using ProductStore.Domain.Commons;
using ProductStore.Domain.Entities.Users;
using ProductStore.Domain.Enums;

namespace ProductStore.Domain.Entities.Orders;

public class Order : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public IEnumerable<OrderItem> OrderItems { get; set; } 
}
