using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PromDresses.Data;

namespace PromDresses.Controllers
{
    [Authorize]
    public class OrderDressesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;    

        public OrderDressesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET: OrderDresses
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var DbContext = _context.OrderDresses
                                    .Include(o => o.Users)
                                    .Include(o => o.Dresses);
                return View(await DbContext.ToListAsync());
            }
            else
            {
                var DbContext = _context.OrderDresses
                                    .Include(o => o.Users)
                                    .Include(o => o.Dresses)
                                    .Where(x => x.UserId == _userManager.GetUserId(User));
                return View(await DbContext.ToListAsync());
            }


        }

        // GET: OrderDresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDress = await _context.OrderDresses
                .Include(o => o.Dresses)
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
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "NameDress");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        public async Task<IActionResult> CreateId(int id)
        {
            OrderDress order = new OrderDress();
            order.DressId = id;
            order.UserId = _userManager.GetUserId(User);
            order.DateRegister = DateTime.Now;
            if (ModelState.IsValid)
            {
               _context.OrderDresses.Add(order);    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DressId"]=new SelectList(_context.OrderDresses, "Id","Name", order.DressId);
            return View(order);  
        }
        // POST: OrderDresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DressId")] OrderDress orderDress)
        {
            orderDress.DateRegister = DateTime.Now;
            orderDress.UserId =_userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(orderDress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "NameDress", orderDress.DressId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
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
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "NameDress", orderDress.DressId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
            return View(orderDress);
        }

        // POST: OrderDresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DressId")] OrderDress orderDress)
        {
            if (id != orderDress.Id)
            {
                return NotFound();
            }
            orderDress.UserId = _userManager.GetUserId(User);
            orderDress.DateRegister = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.OrderDresses.Update(orderDress);
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
            ViewData["DressId"] = new SelectList(_context.Dresses, "Id", "NameDress", orderDress.DressId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", orderDress.UserId);
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
                .Include(o => o.Dresses)
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
