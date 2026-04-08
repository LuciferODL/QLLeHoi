using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLLeHoi.Data;
using QLLeHoi.Models;

namespace QLLeHoi.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly QLLeHoiContext _context;

        public OrganizerController(QLLeHoiContext context)
        {
            _context = context;
        }

        // GET: Organizer
        /*
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizer.ToListAsync());
        }
        */
        //Tim kiem + phan trang
        public async Task<IActionResult> Index(string searchString, string sortOrder, int PageNumber = 1, int PageSize = 3)
        {
            var organizers = from o in _context.Organizer
                             select o;
            if (!String.IsNullOrEmpty(searchString))
            {
                organizers = organizers.Where(s => s.Name!.Contains(searchString));
            }
            //Sap xep
            if(sortOrder=="desc")
            {
                organizers = organizers.OrderByDescending(s => s.Name);
            }
            else
            {
                organizers = organizers.OrderBy(s => s.Name);
            }
            var totalItems = await organizers.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            //lay dl theo trang
            var organizersInPage = await organizers.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            //lay dl cho view thong qua ViewModel
            var viewModel = new OrganizerPageViewModel
            {
                Organizers = organizersInPage,
                SearchString = searchString,
                CurrentPage = PageNumber,
                TotalPages = totalPages,
                SortOrder = sortOrder
            };
            return View(viewModel);
        }
        // GET: Organizer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // GET: Organizer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizerId,Name,Type,Experience")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizer);
        }

        // GET: Organizer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer.FindAsync(id);
            if (organizer == null)
            {
                return NotFound();
            }
            return View(organizer);
        }

        // POST: Organizer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizerId,Name,Type,Experience")] Organizer organizer)
        {
            if (id != organizer.OrganizerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(organizer.OrganizerId))
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
            return View(organizer);
        }

        // GET: Organizer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizer = await _context.Organizer
                .FirstOrDefaultAsync(m => m.OrganizerId == id);
            if (organizer == null)
            {
                return NotFound();
            }

            return View(organizer);
        }

        // POST: Organizer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizer = await _context.Organizer.FindAsync(id);
            if (organizer != null)
            {
                _context.Organizer.Remove(organizer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerExists(int id)
        {
            return _context.Organizer.Any(e => e.OrganizerId == id);
        }
        //Tim kiem
        /*
        public async Task<IActionResult> Search(string searchString)
            {
                var organizers = from o in _context.Organizer
                                 select o;
                if (!String.IsNullOrEmpty(searchString))
                {
                    organizers = organizers.Where(s => s.Name!.Contains(searchString));
                }
                return View(await organizers.ToListAsync());
            }
        }
        */
    }
}

