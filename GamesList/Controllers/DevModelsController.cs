using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesList.Data;
using GamesList.Models;

namespace GamesList.Controllers
{
    public class DevModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DevModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DevModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Devs.ToListAsync());
        }

        // GET: DevModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devModel = await _context.Devs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (devModel == null)
            {
                return NotFound();
            }

            return View(devModel);
        }

        // GET: DevModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DevModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Genre,OpenDate,ClosedDate,DevStatus")] DevModel devModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(devModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(devModel);
        }

        // GET: DevModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devModel = await _context.Devs.FindAsync(id);
            if (devModel == null)
            {
                return NotFound();
            }
            return View(devModel);
        }

        // POST: DevModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Genre,OpenDate,ClosedDate,DevStatus")] DevModel devModel)
        {
            if (id != devModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevModelExists(devModel.ID))
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
            return View(devModel);
        }

        // GET: DevModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var devModel = await _context.Devs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (devModel == null)
            {
                return NotFound();
            }

            return View(devModel);
        }

        // POST: DevModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var devModel = await _context.Devs.FindAsync(id);
            _context.Devs.Remove(devModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevModelExists(int id)
        {
            return _context.Devs.Any(e => e.ID == id);
        }
    }
}
