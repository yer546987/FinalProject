using CasinoApp.Client.Mvc.Data;
using CasinoApp.Client.Mvc.Models;
using CasinoApp.Client.Mvc.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CasinoApp.Client.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CasinoAppClientMvcContext _context;

        public HomeController(ILogger<HomeController> logger, CasinoAppClientMvcContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //DateTime currentTime = DateTime.Now;

            //var tipoComida = await _context.tipoComida
            //    .Where(x => x.Cronograma) 
            //    .ToListAsync();

            //ViewBag.AvailableFoodTypes = tipoComida;

            //var selectedFoodTypeId = HttpContext.Session.GetInt32("SelectedFoodTypeId");
            //ViewBag.SelectedFoodTypeId = selectedFoodTypeId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FindEmployee([FromForm] ModeEF modeEF)
        {
            var identificacion = modeEF.empleado.IdentificacionE;
            DateTime currentTime = DateTime.Now;
            var empleado = await _context.EmpleadoDto
                .Include(x => x.IdGrupoEE) // Asegúrate de que esta relación exista en tu modelo
                .FirstOrDefaultAsync(m => m.IdentificacionE == identificacion);

            if (empleado == null)
            {
                HttpContext.Session.SetString("Mensaje", "El funcionario no se encuentra registrado en el sistema");
                return RedirectToAction("Index");
            }

            // Resto de la lógica del método FindEmployee ...

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
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
    }
}
