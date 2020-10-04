using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUY_Store.Data;
using HUY_Store.Models;
using Microsoft.IdentityModel.Tokens;

namespace HUY_Store.Controllers
{
    public class SneakersController : Controller
    {
        private readonly HUY_StoreContext _context;

        public SneakersController(HUY_StoreContext context)
        {
            _context = context;
        }

        #region Hàm xử lý

        // Hàm đếm số sản phẩm có trong DB
        public int CountId()
        {
            return _context.Sneaker.Select(t => t.ItemId).Count();
        }

        // Hàm tự sinh mã sản phẩm không trùng
        public string AutoCreateItemId(int number)
        {
            if (number < 10)
            {
                string ma = "SP0" + number;
                if (_context.Sneaker.Where(t => t.ItemId == ma).Count() == 0)
                {
                    return ma;
                }
                else
                {
                    return AutoCreateItemId(number + 1);
                }
            }
            else
            {
                string ma = "SP" + number;
                if (_context.Sneaker.Where(t => t.ItemId == ma).Count() == 0)
                {
                    return ma;
                }
                else
                {
                    return AutoCreateItemId(number + 1);
                }
            }
        }

        #endregion
        
        // Chỉ hiển thị các sản phẩm có State(Trình trạng) là 1 nếu State = 0 thì xem như sản phẩm đó đã bị xóa
        // GET: Sneakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sneaker.Where(t=>t.State==1).ToListAsync());
        }

        // GET: Sneakers/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sneaker = await _context.Sneaker.FirstOrDefaultAsync(m => m.ItemId == id);

        //    if (sneaker == null)
        //    {
        //        return NotFound();
        //    }

        //    return PartialView("Details",sneaker);
        //}
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sneaker = await _context.Sneaker.FirstOrDefaultAsync(m => m.ItemId == id);

            if (sneaker == null)
            {
                return NotFound();
            }

            //return View(sneaker);
            return PartialView("DetailPartial",sneaker);
        }


        // GET: Sneakers/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = AutoCreateItemId(CountId());
            return View();
        }

        // POST: Sneakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Size,ColorId,BrandId,Price,Image,Stock,State")] Sneaker sneaker)
        {
            if (ModelState.IsValid)
            {
                sneaker.State = 1; // Mỗi sản phẩm được tạo ra đều được gán State = 1 để hiển thị sự tồn tại của nó
                _context.Add(sneaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sneaker);
        }

        // GET: Sneakers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sneaker = await _context.Sneaker.FindAsync(id);

            if (sneaker == null)
            {
                return NotFound();
            }
            return View(sneaker);
        }

        // POST: Sneakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemId,Name,Size,ColorId,BrandId,Price,Image,Stock,State")] Sneaker sneaker)
        {
            if (id != sneaker.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sneaker.State = 1;
                    _context.Update(sneaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SneakerExists(sneaker.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sneaker);
        }

        // GET: Sneakers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sneaker = await _context.Sneaker
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (sneaker == null)
            {
                return NotFound();
            }
            return View(sneaker);
        }

        // POST: Sneakers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var sneaker = await _context.Sneaker.FindAsync(id);
        //    _context.Sneaker.Remove(sneaker);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        // Hàm delete không thực hiện xóa chỉ chỉnh sửa và gán trình trạng (State) của sản phẩm bằng 0
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id,[Bind("ItemId, Name, Size, ColorId, BrandId, Price, Image, Stock,State")] Sneaker sneaker)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sneaker.State = 0;
                    _context.Update(sneaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SneakerExists(sneaker.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SneakerExists(string id)
        {
            return _context.Sneaker.Any(e => e.ItemId == id);
        }
    }
}
