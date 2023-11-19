using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Products
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string ListingName { get; set; }

        [DisplayName("Description")]
        public string ListingDescription { get; set; }
        public float Price { get; set; } = 1.00f;

        [DisplayName("Base discount")]
        public int BaseDiscount { get; set; } = 0;

        [DisplayName("Stock amount")]
        public int StockAmount { get; set; } = 100;
        public byte[]? Image { get; set; }


        [DisplayName("Listed at")]
        public DateTime ListingDate { get; set; }
    }
}
