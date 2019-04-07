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
    public class GameListModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameListModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameListModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GameListModel.Include(g => g.Game).Include(g => g.List);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GameListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameListModel = await _context.GameListModel
                .Include(g => g.Game)
                .Include(g => g.List)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameListModel == null)
            {
                return NotFound();
            }

            return View(gameListModel);
        }

        // GET: GameListModels/Create
        public IActionResult Create()
        {
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "GameStatus");
            ViewData["ListID"] = new SelectList(_context.Lists, "ListID", "ListID");
            return View();
        }

        // POST: GameListModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,GameID,ListID")] GameListModel gameListModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "GameStatus", gameListModel.GameID);
            ViewData["ListID"] = new SelectList(_context.Lists, "ListID", "ListID", gameListModel.ListID);
            return View(gameListModel);
        }

        // GET: GameListModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameListModel = await _context.GameListModel.FindAsync(id);
            if (gameListModel == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "GameStatus", gameListModel.GameID);
            ViewData["ListID"] = new SelectList(_context.Lists, "ListID", "ListID", gameListModel.ListID);
            return View(gameListModel);
        }

        // POST: GameListModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,GameID,ListID")] GameListModel gameListModel)
        {
            if (id != gameListModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameListModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameListModelExists(gameListModel.ID))
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
            ViewData["GameID"] = new SelectList(_context.Games, "ID", "GameStatus", gameListModel.GameID);
            ViewData["ListID"] = new SelectList(_context.Lists, "ListID", "ListID", gameListModel.ListID);
            return View(gameListModel);
        }

        // GET: GameListModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameListModel = await _context.GameListModel
                .Include(g => g.Game)
                .Include(g => g.List)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameListModel == null)
            {
                return NotFound();
            }

            return View(gameListModel);
        }

        // POST: GameListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameListModel = await _context.GameListModel.FindAsync(id);
            _context.GameListModel.Remove(gameListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameListModelExists(int id)
        {
            return _context.GameListModel.Any(e => e.ID == id);
        }
    }
}
