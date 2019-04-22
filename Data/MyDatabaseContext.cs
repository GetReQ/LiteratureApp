using Microsoft.EntityFrameworkCore;

namespace Literature.Models
{
  public class MyDatabaseContext : DbContext
  {
    public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<Publication> Publication { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


  }
}