using ShopTP22.Models.Products;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.ShoppingCarts
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        [DisplayName("Name")]
        public string ListingName { get; set; }

        [DisplayName("Description")]
        public string ListingDescription { get; set; }
        public float Price { get; set; } = 1.00f;

        [DisplayName("Base discount")]
        public int BaseDiscount { get; set; } = 0;

        public byte[]? Image { get; set; }
    }
}
