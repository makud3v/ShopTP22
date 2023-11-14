using Microsoft.AspNetCore.Mvc;
using ShopTP22.Data;
using ShopTP22.Models.Discounts;
using ShopTP22.Models.Products;

namespace ShopTP22.Controllers
{
    public class DiscountsController : Controller
    {
        private readonly ShopContext _context;

        public DiscountsController(ShopContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Discount> productList = _context.Discounts.ToList();

            return View(productList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            DiscountCreateViewModel vm = new()
            {
                AllProducts = _context.Products.ToList(),
                Products = new List<Product>()
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Create(DiscountCreateViewModel vm, string[] SelectedProducts)
        {
            List<Product> products = new();
            foreach (string productId in SelectedProducts)
            {
                Guid productguid = Guid.Parse(productId);
                Product? product = _context.Products.FirstOrDefault(p => p.Id == productguid);

                if (product != null)
                {
                    products.Add(product);
                }
            }
              

            Discount discount = new()
            {
                Id = vm.Id,
                Percentage = vm.Percentage,
                ExpireDate = vm.ExpireDate,
                Products = products
            }

            await _context.Discounts.AddAsync(discount);
            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}