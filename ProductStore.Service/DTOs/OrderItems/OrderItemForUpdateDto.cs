namespace ProductStore.Service.DTOs.OrderItems;

public record OrderItemForUpdateDto
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
}
