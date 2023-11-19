using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Ratings
{
    public class Rating
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public int Stars { get; set; }
    }
}
