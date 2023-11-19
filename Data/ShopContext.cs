using Microsoft.EntityFrameworkCore;
using ShopTP22.Models.Coupons;
using ShopTP22.Models.Products;
using ShopTP22.Models.Ratings;
using ShopTP22.Models.ShoppingCarts;

namespace ShopTP22.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Coupon> Coupons { get; set;  }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
