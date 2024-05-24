namespace ProductStore.Service.DTOs.Products;

public record ProductForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public long CategoryId { get; set; }
    public string Manufacturer { get; set; }
    public bool Action { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
