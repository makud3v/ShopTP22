using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ShopTP22.Data;
using ShopTP22.Models.ShoppingCarts;

namespace ShopTP22.Controllers
{
    public class BaseController : Controller
    {

        protected readonly ShopContext _context;

        public BaseController(ShopContext context)
        {
            _context = context;
        }


        protected async void UpdateCart()
        {

            string? sessionId = HttpContext.Request.Cookies[".AspNetCore.Session"];
            Cart? shoppingCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.OwnerSessionId == sessionId);

            if (shoppingCart == null)
            {
                shoppingCart = new()
                {
                    OwnerSessionId = sessionId
                };

                await _context.AddAsync(shoppingCart);
                await _context.SaveChangesAsync();
            }


            ViewBag.CartItemCount = shoppingCart.Items?.Count;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UpdateCart();
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
