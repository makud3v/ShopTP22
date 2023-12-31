﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopTP22.Models.Products
{
    public class ProductAdminEditViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string ListingName { get; set; }

        [DisplayName("Description")]
        public string ListingDescription { get; set; }
        public float Price { get; set; }

        [DisplayName("Base discount")]
        public int BaseDiscount { get; set; }

        [DisplayName("Stock amount")]
        public int StockAmount { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageSource { get; set; }
        public List<IFormFile>? Images { get; set; }


        [DisplayName("Listed at")]
        public DateTime ListingDate { get; set; }
    }
}
