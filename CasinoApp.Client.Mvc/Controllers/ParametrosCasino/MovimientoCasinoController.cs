using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Client.Mvc.Models.ViewModels;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class MovimientoCasinoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public MovimientoCasinoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: MovimientoCasino
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<MovimientoCasinoDto>>>("/api/MovimientoCasino");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.TiposEmpleados = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.Empleado = Empleado.ToDictionary(x => x.IdEmpleado, x => x.NombreEmpleado);

                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: MovimientoCasino/Details/5
        public IActionResult Details(int id)
        {
         
            MVAHttpClient client = new MVAHttpClient();
            var movimientoCasinoResult = client.Get<RequestResult<MovimientoCasinoDto>>($"/api/MovimientoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (movimientoCasinoResult.IsSuccessful)
            {
                var movimientoCasino = movimientoCasinoResult.Result;

                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GrupoEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.Empleado = Empleado.ToDictionary(x => x.IdEmpleado, x => x.NombreEmpleado);
                return View(movimientoCasino);
            }

            return NotFound();
        }

        // GET: MovimientoCasino/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;

            var viewModel = new CUMovimientoCasinoViewModel
            {
                grupoEmpleados = gruposEmpleado,
                empleado = Empleado,
                tipoComida = tipoComida
            };

            return View(viewModel);
        }

        // POST: MovimientoCasino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CUMovimientoCasinoViewModel movimientoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<MovimientoCasinoDto>>("/api/MovimientoCasino", movimientoCasinoDto.movimientoCasino);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: MovimientoCasino/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<MovimientoCasinoDto>>($"/api/MovimientoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (resultado.IsSuccessful)
            {
                var viewModel = new CUMovimientoCasinoViewModel
                {
                    movimientoCasino = resultado.Result,
                    tipoComida = tipoComida,
                    grupoEmpleados = gruposEmpleado,
                    empleado = Empleado
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // POST: MovimientoCasino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CUMovimientoCasinoViewModel movimientoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<MovimientoCasinoDto>>("/api/MovimientoCasino/Update", movimientoCasinoDto.movimientoCasino);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: MovimientoCasino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovimientoCasinoDto == null)
            {
                return NotFound();
            }

            var movimientoCasinoDto = await _context.MovimientoCasinoDto
                .FirstOrDefaultAsync(m => m.IdMovimientoCasino == id);
            if (movimientoCasinoDto == null)
            {
                return NotFound();
            }

            return View(movimientoCasinoDto);
        }

        // POST: MovimientoCasino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovimientoCasinoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.MovimientoCasinoDto'  is null.");
            }
            var movimientoCasinoDto = await _context.MovimientoCasinoDto.FindAsync(id);
            if (movimientoCasinoDto != null)
            {
                _context.MovimientoCasinoDto.Remove(movimientoCasinoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoCasinoDtoExists(int id)
        {
          return _context.MovimientoCasinoDto.Any(e => e.IdMovimientoCasino == id);
        }
    }
}
