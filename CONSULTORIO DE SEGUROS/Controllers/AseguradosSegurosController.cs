using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULTORIO_DE_SEGUROS.Models;
using Microsoft.Data.SqlClient;

namespace CONSULTORIO_DE_SEGUROS.Controllers
{
    public class AseguradosSegurosController : Controller
    {
        private readonly ConsultorioSegurosContext _context;

        public AseguradosSegurosController(ConsultorioSegurosContext context)
        {
            _context = context;
        }

        // GET: AseguradosSeguros
        public async Task<IActionResult> Index()
        {
            var consultorioSegurosContext = _context.AseguradosSeguros.Include(a => a.Asegurado).Include(a => a.Seguro);
            return View(await consultorioSegurosContext.ToListAsync());
        }

        // GET: AseguradosSeguros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AseguradosSeguros == null)
            {
                return NotFound();
            }

            var aseguradosSeguros = await _context.AseguradosSeguros
                .Include(a => a.Asegurado)
                .Include(a => a.Seguro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aseguradosSeguros == null)
            {
                return NotFound();
            }

            return View(aseguradosSeguros);
        }

        // GET: AseguradosSeguros/Create
        public IActionResult Create()
        {
            ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente");
            ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro");
            return View();
        }

        // POST: AseguradosSeguros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,AseguradoId,SeguroId")] AseguradosSeguros aseguradosSeguros)
        {
            ModelState.Remove("Asegurado");
            ModelState.Remove("Seguro");
            
             if (ModelState.IsValid)
             {
                try
                {
                    _context.Add(aseguradosSeguros);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;

                    // Verificar si la excepción es debido a la violación de restricción única
                    if (innerException is SqlException sqlException)
                    {
                        // Obtener los nombres de las columnas involucradas en la restricción única
                        var uniqueColumns = new[] { "AseguradoId", "SeguroId" };

                        // Agregar un error personalizado al ModelState
                        foreach (var columnName in uniqueColumns)
                        {
                            ModelState.AddModelError(columnName, "Ya existe una combinación única de AseguradoId y SeguroId.");
                        }

                        // Volver a agregar las opciones del SelectList para las propiedades AseguradoId y SeguroId
                        ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente", aseguradosSeguros.AseguradoId);
                        ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro", aseguradosSeguros.SeguroId);

                        return View(aseguradosSeguros);
                    }

                    // Manejar otras excepciones aquí si es necesario
                    throw;
                }
            }
                ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente");
                ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro");
                return View(aseguradosSeguros);
        }

        // GET: AseguradosSeguros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AseguradosSeguros == null)
            {
                return NotFound();
            }

            var aseguradosSeguros = await _context.AseguradosSeguros
                .Include(a => a.Asegurado)
                .Include(a => a.Seguro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aseguradosSeguros == null)
            {
                return NotFound();
            }
            ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente");
            ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro");
            return View(aseguradosSeguros);
        }

        // POST: AseguradosSeguros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AseguradoId,SeguroId")] AseguradosSeguros aseguradosSeguros)
        {

            if (id != aseguradosSeguros.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Asegurado");
            ModelState.Remove("Seguro");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aseguradosSeguros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;

                    // Verificar si la excepción es debido a la violación de restricción única
                    if (innerException is SqlException sqlException)
                    {
                        // Obtener los nombres de las columnas involucradas en la restricción única
                        var uniqueColumns = new[] { "AseguradoId", "SeguroId" };

                        // Agregar un error personalizado al ModelState
                        foreach (var columnName in uniqueColumns)
                        {
                            ModelState.AddModelError(columnName, "Ya existe una combinación única de AseguradoId y SeguroId.");
                        }

                        // Volver a agregar las opciones del SelectList para las propiedades AseguradoId y SeguroId
                        ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente", aseguradosSeguros.AseguradoId);
                        ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro", aseguradosSeguros.SeguroId);

                        return View(aseguradosSeguros);
                    }
                     
                    // Manejar otras excepciones aquí si es necesario
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AseguradoId"] = new SelectList(_context.Asegurados, "Id", "NombreCliente");
            ViewData["SeguroId"] = new SelectList(_context.Seguros, "Id", "NombreSeguro");
            return View(aseguradosSeguros);
        }

        // GET: AseguradosSeguros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AseguradosSeguros == null)
            {
                return NotFound();
            }

            var aseguradosSeguros = await _context.AseguradosSeguros
                .Include(a => a.Asegurado)
                .Include(a => a.Seguro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aseguradosSeguros == null)
            {
                return NotFound();
            }

            return View(aseguradosSeguros);
        }

        // POST: AseguradosSeguros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AseguradosSeguros == null)
            {
                return Problem("Entity set 'ConsultorioSegurosContext.AseguradosSeguros'  is null.");
            }
            var aseguradosSeguros = await _context.AseguradosSeguros.FindAsync(id);
            if (aseguradosSeguros != null)
            {
                _context.AseguradosSeguros.Remove(aseguradosSeguros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AseguradosSegurosExists(int id)
        {
          return (_context.AseguradosSeguros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
