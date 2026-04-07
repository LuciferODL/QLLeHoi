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
    public class FestivalController : Controller
    {
        private readonly QLLeHoiContext _context;

         public FestivalController(QLLeHoiContext context)
         {
             _context = context;
         }
        //Tim kiem+phan trang
        public async Task<IActionResult> Index(string searchString, string sortOrder, int PageNumber=1,int PageSize=3 )
        {
            var festivals = from f in _context.Festival
                            select f;
            if (!String.IsNullOrEmpty(searchString)) 
            {
                festivals = festivals.Where(s => s.Name.Contains(searchString));
            }
            //sap xep
            if(sortOrder=="desc")
            {
                festivals = festivals.OrderByDescending(s => s.Name);
            }
            else
            {
                festivals = festivals.OrderBy(s => s.Name);
            }
            var totalItems = await festivals.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);
            //lay du lieu theo trang
            var festivalsInPage = await festivals.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
            //lay dl cho view thong qua ViewModel
            var viewModel = new FestivalPageViewModel
            {
                Festivals = festivalsInPage,
                SearchString = searchString,
                CurrentPage = PageNumber,
                TotalPages = totalPages,
                SortOrder = sortOrder
            };
            return View(viewModel);
        }
        // GET: Festival
      /*  public async Task<IActionResult> Index()
        {
            return View(await _context.Festival.ToListAsync());
        }
      */
        // GET: Festival/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festival
                .FirstOrDefaultAsync(m => m.FestivalId == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // GET: Festival/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Festival/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FestivalId,Name,Location,OrganizerId")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                _context.Add(festival);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(festival);
        }

        // GET: Festival/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festival.FindAsync(id);
            if (festival == null)
            {
                return NotFound();
            }
            return View(festival);
        }

        // POST: Festival/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FestivalId,Name,Location,OrganizerId")] Festival festival)
        {
            if (id != festival.FestivalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(festival);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FestivalExists(festival.FestivalId))
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
            return View(festival);
        }

        // GET: Festival/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var festival = await _context.Festival
                .FirstOrDefaultAsync(m => m.FestivalId == id);
            if (festival == null)
            {
                return NotFound();
            }

            return View(festival);
        }

        // POST: Festival/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var festival = await _context.Festival.FindAsync(id);
            if (festival != null)
            {
                _context.Festival.Remove(festival);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FestivalExists(int id)
        {
            return _context.Festival.Any(e => e.FestivalId == id);
        }
    }
}
