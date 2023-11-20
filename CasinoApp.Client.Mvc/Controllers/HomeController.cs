using CasinoApp.Client.Helper;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Client.Mvc.Models;
using CasinoApp.Client.Mvc.Models.ViewModels;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.Usuario;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CasinoApp.Client.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public HomeController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<TipoComidaDto>>>("/api/LogicaCasino");        
            if (resultado.IsSuccessful && resultado.Result != null)
            {
                ViewBag.TipoComida = resultado.Result;
                return View();
            }

            return NotFound();
        }
        public IActionResult Create(CrearMovimiento crearMovimiento)
        {
            MVAHttpClient client = new MVAHttpClient();
       
            var resultado = client.Post<RequestResult<MovimientoCasinoDto>>("/api/LogicaCasino", crearMovimiento);

            if (resultado.IsSuccessful && resultado.Result != null)
            {
                ViewBag.TipoComida = resultado.Result;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
