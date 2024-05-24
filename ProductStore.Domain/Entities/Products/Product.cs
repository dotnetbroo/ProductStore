using ProductStore.Domain.Commons;
using ProductStore.Domain.Entities.Categories;

namespace ProductStore.Domain.Entities.Products;

public class Product : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public string Manufacturer { get; set; }
    public bool Action {  get; set; }
}