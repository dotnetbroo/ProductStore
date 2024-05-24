using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.Entities.Categories;
using ProductStore.Domain.Entities.Orders;
using ProductStore.Domain.Entities.Products;
using ProductStore.Domain.Entities.Reports;
using ProductStore.Domain.Entities.Users;

namespace ProductStore.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
