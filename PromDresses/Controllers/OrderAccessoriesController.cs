using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PromDresses.Data;

namespace PromDresses.Controllers
{
    [Authorize]
    public class OrderAccessoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrderAccessoriesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: OrderAccessories
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var DbContext = _context.OrderAccessories
                                    .Include(o => o.Users)
                                    .Include(o => o.Accessories);
                return View(await DbContext.ToListAsync());
            }
            else
            {
                var DbContext = _context.OrderAccessories
                                    .Include(o => o.Users)
                                    .Include(o => o.Accessories)
                                    .Where(x => x.UserId == _userManager.GetUserId(User));
                return View(await DbContext.ToListAsync());
            }
        }

        // GET: OrderAccessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAccessorie = await _context.OrderAccessories
                .Include(o => o.Accessories)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAccessorie == null)
            {
                return NotFound();
            }

            return View(orderAccessorie);
        }

        // GET: OrderAccessories/Create
        public IActionResult Create()
        {
            ViewData["AccessorieId"] = new SelectList(_context.Accessories, "Id", "NameAccessorie");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        public async Task<IActionResult> CreateId(int id)
        {
            OrderAccessorie order = new OrderAccessorie();
            order.AccessorieId = id;
            order.UserId = _userManager.GetUserId(User);
            order.DateRegister = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.OrderAccessories.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessorieId"] = new SelectList(_context.OrderDresses, "Id", "Name", order.AccessorieId);
            return View(order);
        }
        // POST: OrderAccessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessorieId")] OrderAccessorie orderAccessorie)
        {
            orderAccessorie.DateRegister = DateTime.Now;
            orderAccessorie.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(orderAccessorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccessorieId"] = new SelectList(_context.Accessories, "Id", "NameAccessorie", orderAccessorie.AccessorieId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderAccessorie.UserId);
            return View(orderAccessorie);
        }

        // GET: OrderAccessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAccessorie = await _context.OrderAccessories.FindAsync(id);
            if (orderAccessorie == null)
            {
                return NotFound();
            }
            ViewData["AccessorieId"] = new SelectList(_context.Accessories, "Id", "NameAccessorie", orderAccessorie.AccessorieId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderAccessorie.UserId);
            return View(orderAccessorie);
        }

        // POST: OrderAccessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccessorieId")] OrderAccessorie orderAccessorie)
        {
            if (id != orderAccessorie.Id)
            {
                return NotFound();
            }
            orderAccessorie.DateRegister = DateTime.UtcNow;
            orderAccessorie.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderAccessorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderAccessorieExists(orderAccessorie.Id))
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
            ViewData["AccessorieId"] = new SelectList(_context.Accessories, "Id", "NameAccessorie", orderAccessorie.AccessorieId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderAccessorie.UserId);
            return View(orderAccessorie);
        }

        // GET: OrderAccessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderAccessorie = await _context.OrderAccessories
                .Include(o => o.Accessories)
                .Include(o => o.Users)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderAccessorie == null)
            {
                return NotFound();
            }

            return View(orderAccessorie);
        }

        // POST: OrderAccessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderAccessorie = await _context.OrderAccessories.FindAsync(id);
            if (orderAccessorie != null)
            {
                _context.OrderAccessories.Remove(orderAccessorie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderAccessorieExists(int id)
        {
            return _context.OrderAccessories.Any(e => e.Id == id);
        }
    }
}
