using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarbandOfTheSpiritborn.Data;

namespace WarbandOfTheSpiritborn.Controllers
{
    public class ElementalistBuildsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ElementalistBuildsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Ele Builds
        public async Task<IActionResult> Index()
        {
            return View(await _context.Events.ToListAsync());
        }
       
        // GET: Events/ElementalistBuilds
        public async Task<IActionResult> ElementalistBuilds(String Phrase)
        {
            return View("Index", await _context.Builds.Where(j => j.Profession.Contains(Phrase)).ToListAsync()); //Using an arrow function to filter results
        }
    }
}
