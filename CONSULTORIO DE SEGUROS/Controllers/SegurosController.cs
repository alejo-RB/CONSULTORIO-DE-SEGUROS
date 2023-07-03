using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULTORIO_DE_SEGUROS.Models;

namespace CONSULTORIO_DE_SEGUROS.Controllers
{
    public class SegurosController : Controller
    {
        private readonly ConsultorioSegurosContext _context;

        public SegurosController(ConsultorioSegurosContext context)
        {
            _context = context;
        }

        // GET: Seguros
        public async Task<IActionResult> Index()
        {
              return _context.Seguros != null ? 
                          View(await _context.Seguros.ToListAsync()) :
                          Problem("Entity set 'ConsultorioSegurosContext.Seguros'  is null.");
        }

        // GET: Seguros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seguros == null)
            {
                return NotFound();
            }

            return View(seguros);
        }

        // GET: Seguros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seguros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreSeguro,CodigoSeguro,SumaAsegurada,Prima")] Seguros seguros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seguros);
        }

        // GET: Seguros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros.FindAsync(id);
            if (seguros == null)
            {
                return NotFound();
            }
            return View(seguros);
        }

        // POST: Seguros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreSeguro,CodigoSeguro,SumaAsegurada,Prima")] Seguros seguros)
        {
            if (id != seguros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegurosExists(seguros.Id))
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
            return View(seguros);
        }

        // GET: Seguros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seguros == null)
            {
                return NotFound();
            }

            var seguros = await _context.Seguros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seguros == null)
            {
                return NotFound();
            }

            return View(seguros);
        }

        // POST: Seguros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seguros == null)
            {
                return Problem("Entity set 'ConsultorioSegurosContext.Seguros'  is null.");
            }
            var seguros = await _context.Seguros.FindAsync(id);
            if (seguros != null)
            {
                _context.Seguros.Remove(seguros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegurosExists(int id)
        {
          return (_context.Seguros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
