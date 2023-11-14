using ShopTP22.Models.Products;
using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Discounts
{
    public class Discount
    {
        [Key]
        public Guid Id { get; set; }

        public int Percentage { get; set; }

        public DateTime? ExpireDate { get; set;  }
        public IEnumerable<Product>? Products { get; set;  }
    }
}
