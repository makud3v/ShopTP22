using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTP22.Data;
using ShopTP22.Models;
using ShopTP22.Models.Products;
using ShopTP22.Models.ShoppingCarts;
using System.Diagnostics;

namespace ShopTP22.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ShopContext context) : base(context)
        {
        }



        public async Task<IActionResult> Index()
        {
            List<ProductHomeIndexViewModel> vm = await _context.Products.Select(p => (
                new ProductHomeIndexViewModel()
                {
                    Id = p.Id,
                    ListingName = p.ListingName,
                    Price = p.Price,
                    BaseDiscount = p.BaseDiscount,
                    Image = p.Image,
                    ListingDate = p.ListingDate,
                    StockAmount = p.StockAmount,
                    ImageSource = p.Image != null ? Utility.BytesToImageSource(p.Image) : null
                }
            )).ToListAsync();


            return View(vm);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            Product? product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return BadRequest("Invalid product when trying to view details, (id " + id.ToString() + ")");
            }

            int totalRating = 0;
            int ratingCount = 0;
            foreach (var rating in await _context.Ratings
                .Where(r => r.ProductId == id)
                .ToListAsync())
            {
                totalRating += rating.Stars;
                ratingCount++;
            }

            ViewBag.AverageRating = ratingCount > 0 ? (totalRating / ratingCount) : 0;
            ViewBag.ImageSource = product.Image != null ? Utility.BytesToImageSource(product.Image) : "https://via.placeholder.com/300";
            return View(product);
        }
    }
}