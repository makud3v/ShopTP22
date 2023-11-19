using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTP22.Data;
using ShopTP22.Models.Coupons;
using ShopTP22.Models.Products;
using ShopTP22.Models.ShoppingCarts;

namespace ShopTP22.Controllers
{
    public class ShoppingCartsController : BaseController
    {

        public ShoppingCartsController(ShopContext context) : base(context)
        {
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? sessionId = HttpContext.Request.Cookies[".AspNetCore.Session"];
            Cart? shoppingCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.OwnerSessionId == sessionId);

            if (shoppingCart == null)
            {
                return NotFound();
            }


            ViewBag.Images = new HashSet<KeyValuePair<Guid, string>>();
            foreach (CartItem item in shoppingCart.Items)
            {
                if (item.Image != null)
                {
                    ViewBag.Images.Add(new KeyValuePair<Guid, string>(
                        item.Id,
                        Utility.BytesToImageSource(item.Image)
                    ));
                }
            }


            return View(shoppingCart);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid productId)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                return BadRequest("Invalid product");
            }

            if (product.StockAmount <= 0)
            {
                return BadRequest("Out of stock for product");
            }

            string? sessionId = HttpContext.Request.Cookies[".AspNetCore.Session"];
            Cart? shoppingCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.OwnerSessionId == sessionId);

            CartItem item = new()
            {
                Id = Guid.NewGuid(),
                Cart = shoppingCart,
                CartId = shoppingCart.Id,
                ProductId = productId,
                ListingName = product.ListingName,
                ListingDescription = product.ListingDescription,
                Price = product.Price,
                BaseDiscount = product.BaseDiscount,
                Image = product.Image
            };

            product.StockAmount--;
            shoppingCart.Items.Add(item);
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Home");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(Guid itemId)
        {
            CartItem? item = await _context.CartItems
                .FirstOrDefaultAsync(i => i.Id == itemId);
            if (item == null)
            {
                return BadRequest("Invalid item");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
            product.StockAmount++;
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Checkout(Guid id)
        {
            Cart? cart = await _context.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return BadRequest("Invalid cart");
            }


            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(Guid id, string? coupon)
        {
            Guid cartId = id;
            Cart? cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null)
            {
                return BadRequest("Invalid cart when applying coupon");
            }

            Coupon? cpn = await _context.Coupons.FirstOrDefaultAsync(cpn => cpn.Name == coupon);
            if (cpn != null && DateTime.Now < cpn.ExpireDate)
            {
                foreach (CartItem item in cart.Items)
                {
                    item.BaseDiscount = Math.Clamp(item.BaseDiscount + (int)cpn.Percentage, 0, 100);
                }

                _context.Coupons.Remove(cpn);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Checkout", new { id = cartId });
        }
    }
}
