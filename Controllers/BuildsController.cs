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
        // GET: EleBuilds
        public async Task<IActionResult> ElementalistBuilds()
        {
            return View(await _context.Builds.ToListAsync());
        }
        // GET: Builds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var builds = await _context.Builds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (builds == null)
            {
                return NotFound();
            }

            return View(builds);
        }
        
        // GET: Builds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Builds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, BuildName, ShortDescription, BuildAuthor, Item, Stat, WeaponSet, OtherItems, Rotation, MainSkills, SecondarySkills, BuildDate")] Builds announcements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcements);
        }

        // GET: Builds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcements = await _context.Builds.FindAsync(id);
            if (announcements == null)
            {
                return NotFound();
            }
            return View(announcements);
        }

        // POST: Builds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, BuildName, ShortDescription, BuildAuthor, Item, Stat, WeaponSet, OtherItems, Rotation, MainSkills, SecondarySkills, BuildDate")] Builds announcements)
        {
            if (id != announcements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementsExists(announcements.Id))
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
            return View(announcements);
        }

        // GET: Builds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcements = await _context.Builds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (announcements == null)
            {
                return NotFound();
            }

            return View(announcements);
        }

        // POST: Builds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcements = await _context.Builds.FindAsync(id);
            _context.Builds.Remove(announcements);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementsExists(int id)
        {
            return _context.Builds.Any(e => e.Id == id);
        }
    }
}
