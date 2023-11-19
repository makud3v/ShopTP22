using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTP22.Data;
using ShopTP22.Models.Coupons;

namespace ShopTP22.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ShopContext _context;

        public CouponsController(ShopContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<Coupon> coupons = _context.Coupons.ToList();

            return View(coupons);
        }


        [HttpGet]
        public IActionResult Create()
        {
            Coupon coupon = new();

            return View(coupon);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (coupon == null)
            {
                return BadRequest("Missing coupon");
            }


            await _context.Coupons.AddAsync(coupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
            if (coupon == null)
            {
                return BadRequest("Invalid coupon");
            }


            return View(coupon);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            Coupon? couponInstance = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == coupon.Id);
            if (couponInstance == null)
            {
                return BadRequest("Invalid coupon");
            }

            if (await TryUpdateModelAsync(couponInstance))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return BadRequest(ModelState.ValidationState);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Coupon? coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == id);
            if (coupon == null)
            {
                return BadRequest("Invalid coupon");
            }

            return View(coupon);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Coupon coupon)
        {
            Coupon? couponInstance = await _context.Coupons.FirstOrDefaultAsync(c => c.Id == coupon.Id);
            if (couponInstance == null)
            {
                return BadRequest("Invalid coupon");
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}