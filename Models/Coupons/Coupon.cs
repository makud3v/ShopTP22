using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Coupons
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
