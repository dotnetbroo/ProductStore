using ProductStore.Domain.Commons;
using ProductStore.Domain.Entities.Products;

namespace ProductStore.Domain.Entities.Orders;

public class OrderItem : Auditable
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}