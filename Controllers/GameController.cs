using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Buteanu_Andrei.Data;
using Proiect_Buteanu_Andrei.Models;

namespace Proiect_Buteanu_Andrei.Controllers
{
    public class gamesController : Controller
    {
        private readonly StoreContext _context;

        public gamesController(StoreContext context)
        {
            _context = context;
        }

        // GET: games
        public async Task<IActionResult> Index(
  string sortOrder,
  string currentFilter,
  string searchString,
  int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var games = from b in _context.Games
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    games = games.OrderByDescending(b => b.Title);
                    break;
                case "Price":
                    games = games.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    games = games.OrderByDescending(b => b.Price);
                    break;
                default:
                    games = games.OrderBy(b => b.Title);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
            .Include(s => s.Orders)
            .ThenInclude(e => e.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Developer,Publisher,Price")] Game game)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(game);
        }

        // GET: games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Games.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Game>(
            studentToUpdate,
            "",
            s => s.Developer, s => s.Publisher, s => s.Title, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }


        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (game == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(game);
        }

        // POST: games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool gameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
    }
}