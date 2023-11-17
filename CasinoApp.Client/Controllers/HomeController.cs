using CasinoApp.Client.Helper;
using CasinoApp.Client.Models;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CasinoApp.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<EmpleadoDto>>>("/Api/Empleado");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
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