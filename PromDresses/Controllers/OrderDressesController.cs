using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromDresses.Data;

namespace PromDresses.Controllers
{
    public class OrderDressesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDressesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderDresses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderDresses.Include(o => o.Dress).Include(o => o.Users);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderDresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDress = await _context.OrderDresses
                .Include(o => o.Dress)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDress == null)
            {
                return NotFound();
            }

            return View(orderDress);
        }

        // GET: OrderDresses/Create
        public IActionResult Create()
        {
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: OrderDresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,DressId,DateRegister")] OrderDress orderDress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "Id", orderDress.DressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
            return View(orderDress);
        }

        // GET: OrderDresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDress = await _context.OrderDresses.FindAsync(id);
            if (orderDress == null)
            {
                return NotFound();
            }
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "Id", orderDress.DressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
            return View(orderDress);
        }

        // POST: OrderDresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,DressId,DateRegister")] OrderDress orderDress)
        {
            if (id != orderDress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDressExists(orderDress.Id))
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
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "Id", orderDress.DressId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
            return View(orderDress);
        }

        // GET: OrderDresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDress = await _context.OrderDresses
                .Include(o => o.Dress)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderDress == null)
            {
                return NotFound();
            }

            return View(orderDress);
        }

        // POST: OrderDresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDress = await _context.OrderDresses.FindAsync(id);
            if (orderDress != null)
            {
                _context.OrderDresses.Remove(orderDress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDressExists(int id)
        {
            return _context.OrderDresses.Any(e => e.Id == id);
        }
    }
}
