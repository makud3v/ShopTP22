using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Coupons
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Percentage { get; set; }

        [DisplayName("Expiration date")]
        public DateTime ExpireDate { get; set; }
    }
}
