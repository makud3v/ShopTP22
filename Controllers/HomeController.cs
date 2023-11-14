using Microsoft.AspNetCore.Mvc;
using ShopTP22.Data;
using ShopTP22.Models;
using ShopTP22.Models.Products;
using System.Diagnostics;

namespace ShopTP22.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopContext _context;

        public HomeController(ShopContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            List<ProductHomeIndexViewModel> vm = _context.Products.Select(p => (
                new ProductHomeIndexViewModel()
                {
                    Id = p.Id,
                    ListingName = p.ListingName,
                    Price = p.Price,
                    BaseDiscount = p.BaseDiscount,
                    Image = p.Image,
                    ListingDate = p.ListingDate
                }
            )).ToList();

            return View(vm);
        }
    }
}