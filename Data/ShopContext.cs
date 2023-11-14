using Microsoft.EntityFrameworkCore;
using ShopTP22.Models.Coupons;
using ShopTP22.Models.Discounts;
using ShopTP22.Models.Products;

namespace ShopTP22.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set;  }
        public DbSet<Coupon> Coupons { get; set;  }
    }
}
