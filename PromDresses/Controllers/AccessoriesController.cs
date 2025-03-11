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
    public class AccessoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccessoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accessories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Accessories.Include(a => a.Collections);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Accessories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessorie = await _context.Accessories
                .Include(a => a.Collections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessorie == null)
            {
                return NotFound();
            }

            return View(accessorie);
        }

        // GET: Accessories/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Name");
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CNumber,NameAccessorie,CollectionId,Description,URLimages,Price,DateRegister")] Accessorie accessorie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accessorie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", accessorie.CollectionId);
            return View(accessorie);
        }

        // GET: Accessories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessorie = await _context.Accessories.FindAsync(id);
            if (accessorie == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", accessorie.CollectionId);
            return View(accessorie);
        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CNumber,NameAccessorie,CollectionId,Description,URLimages,Price,DateRegister")] Accessorie accessorie)
        {
            if (id != accessorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessorie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessorieExists(accessorie.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.Collections, "Id", "Id", accessorie.CollectionId);
            return View(accessorie);
        }

        // GET: Accessories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessorie = await _context.Accessories
                .Include(a => a.Collections)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accessorie == null)
            {
                return NotFound();
            }

            return View(accessorie);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessorie = await _context.Accessories.FindAsync(id);
            if (accessorie != null)
            {
                _context.Accessories.Remove(accessorie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessorieExists(int id)
        {
            return _context.Accessories.Any(e => e.Id == id);
        }
    }
}
