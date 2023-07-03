using CONSULTORIO_DE_SEGUROS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace CONSULTORIO_DE_SEGUROS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConsultorioSegurosContext _context;

        public HomeController(ILogger<HomeController> logger, ConsultorioSegurosContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult BuscarPorCedula(string cedula)
        {
            var resultados = _context.AseguradosSeguros
                .Where(a => a.Asegurado.Cedula == cedula)
                .Select(a => new
                {
                    Cedula = a.Asegurado.Cedula,
                    NombreCliente = a.Asegurado.NombreCliente,
                    NombreSeguro = a.Seguro.NombreSeguro
                })
                .ToList();

            ViewBag.ResultadosPorCedula = resultados;

            if (resultados.Count > 0)
            {
                return View("Index");
            }
            else
            {
                ViewBag.CedulaNoEncontrada = cedula;
                return View("NoResults");
            }
        }

        [HttpGet]
        public IActionResult BuscarPorCodigoSeguro(string codigoSeguro)
        {
            var resultados = _context.AseguradosSeguros
                .Where(a => a.Seguro.CodigoSeguro == codigoSeguro)
                .Select(a => new
                {
                    Cedula = a.Asegurado.Cedula,
                    NombreCliente = a.Asegurado.NombreCliente,
                    NombreSeguro = a.Seguro.NombreSeguro
                })
                .ToList();

            ViewBag.ResultadosPorCodigoSeguro = resultados;

            if (resultados.Count > 0)
            {
                return View("Index");
            }
            else
            {
                ViewBag.CodigoSeguroNoEncontrado = codigoSeguro;
                return View("NoResults");
            }
        }

    }
}
