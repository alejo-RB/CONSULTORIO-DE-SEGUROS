using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CONSULTORIO_DE_SEGUROS.Models;
using ExcelDataReader;

namespace CONSULTORIO_DE_SEGUROS.Controllers
{
    public class AseguradosController : Controller
    {
        private readonly ConsultorioSegurosContext _context;

        public AseguradosController(ConsultorioSegurosContext context)
        {
            _context = context;
        }

        public IActionResult DatosMasivos()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DatosMasivos(IFormFile fileInput)
        {
            if (fileInput != null && fileInput.Length > 0)
            {
                if (fileInput.FileName.EndsWith(".xlsx"))
                {
                    using (var stream = fileInput.OpenReadStream())
                    {
                        // Utilizar ExcelDataReader para leer el archivo Excel (.xlsx)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Avanzar al resultado de la primera hoja del archivo Excel
                            reader.Read();

                            // Procesar los datos masivamente
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila y realizar las operaciones necesarias
                                var cedula = reader.GetValue(0)?.ToString();
                                var nombreCliente = reader.GetValue(1)?.ToString();
                                var telefono = reader.GetValue(2)?.ToString();
                                var edad = Convert.ToInt32(reader.GetValue(3));

                                // Ejemplo de creación de un nuevo objeto Asegurado y guardar en la base de datos
                                var asegurado = new Asegurados
                                {
                                    Cedula = cedula,
                                    NombreCliente = nombreCliente,
                                    Telefono = telefono,
                                    Edad = edad
                                };
                                _context.Asegurados.Add(asegurado);
                            }

                            _context.SaveChanges();
                        }
                    }
                }
                else if (fileInput.FileName.EndsWith(".txt"))
                {
                    using (var reader = new StreamReader(fileInput.OpenReadStream()))
                    {
                        // Utilizar System.IO para leer el archivo de texto (.txt)
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Separar los datos de la línea en campos individuales
                            string[] campos = line.Split(',');

                            if (campos.Length == 4)
                            {
                                // Obtener los valores de los campos
                                string cedula = campos[0];
                                string nombreCliente = campos[1];
                                string telefono = campos[2];
                                int edad;
                                if (int.TryParse(campos[3], out edad))
                                {
                                    // Ejemplo de creación de un nuevo objeto Asegurado y guardar en la base de datos
                                    var asegurado = new Asegurados
                                    {
                                        Cedula = cedula,
                                        NombreCliente = nombreCliente,
                                        Telefono = telefono,
                                        Edad = edad
                                    };
                                    _context.Asegurados.Add(asegurado);
                                }
                                else
                                {
                                    // La edad no es un número válido, puedes manejarlo de acuerdo a tus necesidades
                                    // Por ejemplo, podrías agregar un registro de error a un archivo de registro
                                }
                            }
                            else
                            {
                                // La línea no tiene el formato esperado, puedes manejarlo de acuerdo a tus necesidades
                                // Por ejemplo, podrías agregar un registro de error a un archivo de registro
                            }
                        }

                        _context.SaveChanges();
                    }
                }
                else
                {
                    // El tipo de archivo no es compatible, mostrar un mensaje de error
                    ModelState.AddModelError("", "El tipo de archivo no es compatible. Por favor, selecciona un archivo .xlsx o .txt.");
                }

                // Redirigir o mostrar mensaje de éxito
                return RedirectToAction("Index", "Asegurados");
            }

            // Si no se seleccionó ningún archivo, mostrar un mensaje de error
            ModelState.AddModelError("", "Por favor, selecciona un archivo.");

            return View();
        }


        // GET: Asegurados
        public async Task<IActionResult> Index()
        {
              return _context.Asegurados != null ? 
                          View(await _context.Asegurados.ToListAsync()) :
                          Problem("Entity set 'ConsultorioSegurosContext.Asegurados'  is null.");
        }

        // GET: Asegurados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asegurados == null)
            {
                return NotFound();
            }

            var asegurados = await _context.Asegurados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asegurados == null)
            {
                return NotFound();
            }

            return View(asegurados);
        }

        // GET: Asegurados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Asegurados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,NombreCliente,Telefono,Edad")] Asegurados asegurados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asegurados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(asegurados);
        }

        // GET: Asegurados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asegurados == null)
            {
                return NotFound();
            }

            var asegurados = await _context.Asegurados.FindAsync(id);
            if (asegurados == null)
            {
                return NotFound();
            }
            return View(asegurados);
        }

        // POST: Asegurados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,NombreCliente,Telefono,Edad")] Asegurados asegurados)
        {
            if (id != asegurados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asegurados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AseguradosExists(asegurados.Id))
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
            return View(asegurados);
        }

        // GET: Asegurados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asegurados == null)
            {
                return NotFound();
            }

            var asegurados = await _context.Asegurados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asegurados == null)
            {
                return NotFound();
            }

            return View(asegurados);
        }

        // POST: Asegurados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asegurados == null)
            {
                return Problem("Entity set 'ConsultorioSegurosContext.Asegurados'  is null.");
            }
            var asegurados = await _context.Asegurados.FindAsync(id);
            if (asegurados != null)
            {
                _context.Asegurados.Remove(asegurados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AseguradosExists(int id)
        {
          return (_context.Asegurados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
