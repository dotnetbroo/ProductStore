using ProductStore.Domain.Commons;
using ProductStore.Domain.Entities.Products;

namespace ProductStore.Domain.Entities.Categories;

public class Category : Auditable
{
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
