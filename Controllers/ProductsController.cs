using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTP22.Data;
using ShopTP22.Models;
using ShopTP22.Models.Products;
using ShopTP22.Models.Ratings;
using ShopTP22.Models.ShoppingCarts;

namespace ShopTP22.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;


        public ProductsController(ShopContext context)
        {
           _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<ProductAdminIndexViewModel> vm = _context.Products.Select(p => (            
                new ProductAdminIndexViewModel()
                {
                    Id = p.Id,
                    ListingName = p.ListingName,
                    ListingDescription = p.ListingDescription,
                    Price = p.Price,
                    StockAmount = p.StockAmount,
                    BaseDiscount = p.BaseDiscount,
                    Image = p.Image,
                    ListingDate = p.ListingDate
                }
            )).ToList();

            return View(vm);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProductAdminCreateViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductAdminCreateViewModel vm)
        {
            if (vm == null)
            {
                return BadRequest("Missing product");
            }
            Product product = new()
            {
                Id = Guid.NewGuid(),
                ListingName = vm.ListingName,
                ListingDescription = vm.ListingDescription,
                Price = vm.Price,
                BaseDiscount = vm.BaseDiscount,
                StockAmount = vm.StockAmount,
                ListingDate = vm.ListingDate
            };

            using (var stream = new MemoryStream())
            {
                var file = vm.Images?[0];
                if (file != null)
                {
                    file.CopyTo(stream);

                    product.Image = stream.ToArray();
                }
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            ProductAdminEditViewModel vm = new()
            {
                Id = Guid.NewGuid(),
                ListingName = product.ListingName,
                ListingDescription = product.ListingDescription,
                Price = product.Price,
                BaseDiscount = product.BaseDiscount,
                StockAmount = product.StockAmount,
                ListingDate = product.ListingDate
            };
            if (product.Image != null)
            {
                vm.Image = product.Image;
                vm.ImageSource = Utility.BytesToImageSource(product.Image);
            }

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProductAdminEditViewModel vm)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.Id == vm.Id);
            if (vm.Images != null)
            {
                product.Image = Utility.FormFileToBytes(vm.Images?[0]);
            }

            if (await TryUpdateModelAsync(product))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest(ModelState.ValidationState);
            }
        }



        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Product? product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return BadRequest("Missing product");
            }

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            Product? pr = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (pr == null)
            {
                return BadRequest("Missing product");
            }

            _context.Products.Remove(pr);
            await _context.SaveChangesAsync();


            foreach (var item in await _context.CartItems
                .Where(ci => ci.ProductId == pr.Id)
                .ToListAsync()
            )
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RateProduct(Guid id, int rating)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return BadRequest("Invalid product when trying to rate");
            }

            Rating newRating = new()
            {
                Id = Guid.NewGuid(),
                Stars = Math.Clamp(rating, 0, 5),
                ProductId = id
            };

            await _context.Ratings.AddAsync(newRating);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}