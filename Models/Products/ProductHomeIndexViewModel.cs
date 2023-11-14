using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Products
{
    public class ProductHomeIndexViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string ListingName { get; set; }
        public float Price { get; set; }
        public int BaseDiscount { get; set; }
        public int StockAmount { get; set; }
        public byte[] Image { get; set; }

        public DateTime ListingDate { get; set; }
    }
}
