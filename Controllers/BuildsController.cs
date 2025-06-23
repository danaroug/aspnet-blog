using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarbandOfTheSpiritborn.Data;
using WarbandOfTheSpiritborn.Models;

namespace WarbandOfTheSpiritborn.Controllers
{
    public class BuildsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuildsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Builds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Builds.ToListAsync());
        }

        // GET: Builds/ByProfession?profession=Elementalist
        public async Task<IActionResult> ByProfession(string profession)
        {
            if (string.IsNullOrEmpty(profession))
            {
                return NotFound();
            }

            var builds = await _context.Builds
                .Where(b => b.Profession != null &&
                b.Profession.Trim().ToLower() == profession.Trim().ToLower())
                .ToListAsync();

            ViewData["Title"] = $"{profession} Builds";
            ViewData["Profession"] = profession;

            // Use a generic view "ByProfession" to avoid confusion with profession-specific views
            return View("ByProfession", builds);
        }

        // GET: Builds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var build = await _context.Builds.FirstOrDefaultAsync(m => m.Id == id);
            if (build == null)
                return NotFound();

            return View(build);
        }

        // GET: Builds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Builds/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BuildName,Profession,ShortDescription,BuildAuthor,Item,Stat,WeaponSet,OtherItems,Rotation,MainSkills,SecondarySkills,BuildDate")] Builds build)
        {
            if (ModelState.IsValid)
            {
                _context.Add(build);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(build);
        }

        // GET: Builds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var build = await _context.Builds.FindAsync(id);
            if (build == null)
                return NotFound();

            return View(build);
        }

        // POST: Builds/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BuildName,Profession,ShortDescription,BuildAuthor,Item,Stat,WeaponSet,OtherItems,Rotation,MainSkills,SecondarySkills,BuildDate")] Builds build)
        {
            if (id != build.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(build);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildExists(build.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(build);
        }

        // GET: Builds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var build = await _context.Builds.FirstOrDefaultAsync(m => m.Id == id);
            if (build == null)
                return NotFound();

            return View(build);
        }

        // POST: Builds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var build = await _context.Builds.FindAsync(id);
            _context.Builds.Remove(build);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildExists(int id)
        {
            return _context.Builds.Any(e => e.Id == id);
        }
    }
}
