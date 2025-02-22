using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalYearProject.Data;
using FinalYearProject.Models;
using FinalYearProject.ViewModels;
using FinalYearProject.Services;

namespace FinalYearProject.Controllers
{
    public class BakesController : Controller
    {
        private readonly IBakeService _service;

        public BakesController(IBakeService service)
        {
            _service = service;
        }

        // GET: Bakes
        public async Task<IActionResult> Index()
        {
            return View(_service.GetAllBakes());
        }

        //// GET: Bakes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bake = await _context.Bake
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bake == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bake);
        //}

        // GET: Bakes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,Description")] BakeFrontEnd bake)
        {
            // Frontend bake model passed through to service layer
            if (ModelState.IsValid)
            {
                _service.AddBake(bake);
            }
            return View(bake);
        }

        //// GET: Bakes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bake = await _context.Bake.FindAsync(id);
        //    if (bake == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(bake);
        //}

        //// POST: Bakes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,Description")] Bake bake)
        //{
        //    if (id != bake.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bake);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BakeExists(bake.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(bake);
        //}

        //// GET: Bakes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bake = await _context.Bake
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bake == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bake);
        //}

        //// POST: Bakes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var bake = await _context.Bake.FindAsync(id);
        //    if (bake != null)
        //    {
        //        _context.Bake.Remove(bake);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BakeExists(int id)
        //{
        //    return _context.Bake.Any(e => e.Id == id);
        //}
    }
}
