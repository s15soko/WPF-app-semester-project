using System.Data.Entity;
using WpfApp_3SemesterApp.Models;

namespace WpfApp_3SemesterApp.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
