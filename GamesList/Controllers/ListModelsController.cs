using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesList.Data;
using GamesList.Models;
using Microsoft.AspNetCore.Authorization;


namespace GamesList.Controllers
{

    [Authorize]
    public class ListModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lists.Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listModel = await _context.Lists
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ListID == id);
            if (listModel == null)
            {
                return NotFound();
            }

            return View(listModel);
        }

        // GET: ListModels/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ListModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListID,Status,UserID")] ListModel listModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", listModel.UserID);
            return View(listModel);
        }

        // GET: ListModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listModel = await _context.Lists.FindAsync(id);
            if (listModel == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", listModel.UserID);
            return View(listModel);
        }

        // POST: ListModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListID,Status,UserID")] ListModel listModel)
        {
            if (id != listModel.ListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListModelExists(listModel.ListID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", listModel.UserID);
            return View(listModel);
        }

        // GET: ListModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listModel = await _context.Lists
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.ListID == id);
            if (listModel == null)
            {
                return NotFound();
            }

            return View(listModel);
        }

        // POST: ListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listModel = await _context.Lists.FindAsync(id);
            _context.Lists.Remove(listModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListModelExists(int id)
        {
            return _context.Lists.Any(e => e.ListID == id);
        }
    }
}
