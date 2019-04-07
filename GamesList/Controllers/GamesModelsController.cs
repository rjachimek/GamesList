using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesList.Data;
using GamesList.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GamesList.Controllers
{
	public class GamesModelsController : Controller
	{
		private readonly ApplicationDbContext _context;

		private readonly UserManager<IdentityUser> _userManager;
		public GamesModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: GamesModels
		public async Task<IActionResult> Index()
		{
			return View(await _context.Games.ToListAsync());
		}

		// GET: GamesModels/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var gamesModel = await _context.Games
				.FirstOrDefaultAsync(m => m.ID == id);
			if (gamesModel == null)
			{
				return NotFound();
			}

			return View(gamesModel);
		}

		// GET: GamesModels/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: GamesModels/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name,Genre,GameStatus,PremiereDate,DeveloperID,Hltb,Hltb100,Photo")] GamesModel gamesModel, IFormFile photo)
		{
			if (ModelState.IsValid)
			{

				_context.Add(gamesModel);
				await _context.SaveChangesAsync();
               
                if (photo == null)
				{
					return RedirectToAction(nameof(Index));
				}
				else
				{
					var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", photo.FileName);
					var stream = new FileStream(path, FileMode.Create);
					await photo.CopyToAsync(stream);
					gamesModel.Photo = photo.FileName;
				}




				return RedirectToAction(nameof(Index));
			}

			return View(gamesModel);
		}

		// GET: GamesModels/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var gamesModel = await _context.Games.FindAsync(id);
			if (gamesModel == null)
			{
				return NotFound();
			}
			return View(gamesModel);
		}

		// POST: GamesModels/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Genre,GameStatus,PremiereDate,DeveloperID,Hltb,Hltb100,Photo")] GamesModel gamesModel)
		{
			if (id != gamesModel.ID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(gamesModel);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GamesModelExists(gamesModel.ID))
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
			return View(gamesModel);
		}






		// GET: GamesModels/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var gamesModel = await _context.Games
				.FirstOrDefaultAsync(m => m.ID == id);
			if (gamesModel == null)
			{
				return NotFound();
			}

			return View(gamesModel);
		}






		private bool GamesModelExists(int id)
		{
			return _context.Games.Any(e => e.ID == id);
		}
	}
}
