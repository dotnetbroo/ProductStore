namespace ProductStore.Service.DTOs.Products;

public record CategoryForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public ICollection<ProductForResultDto> Products { get; set; }
}
