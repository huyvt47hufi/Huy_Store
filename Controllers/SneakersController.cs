using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HUY_Store.Data;
using HUY_Store.Models;

namespace HUY_Store.Controllers
{
    public class SneakersController : Controller
    {
        private readonly HUY_StoreContext _context;

        public SneakersController(HUY_StoreContext context)
        {
            _context = context;
        }

        // GET: Sneakers
        public async Task<IActionResult> Index()
        { 

            return View(await _context.Sneaker.ToListAsync());
        }

        // GET: Sneakers/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Sneakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sneakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Price,Image")] Sneaker sneaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sneaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sneaker);
        }

        // GET: Sneakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Name,Price,Image")] Sneaker sneaker)
        {
            if (id != sneaker.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
        public async Task<IActionResult> Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sneaker = await _context.Sneaker.FindAsync(id);
            _context.Sneaker.Remove(sneaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SneakerExists(int id)
        {
            return _context.Sneaker.Any(e => e.ItemId == id);
        }
    }
}
