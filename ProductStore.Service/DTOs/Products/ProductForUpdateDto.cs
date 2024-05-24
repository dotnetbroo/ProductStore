namespace ProductStore.Service.DTOs.Products;

public record ProductForUpdateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public long CategoryId { get; set; }
    public string Manufacturer { get; set; }
}
