using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Client.Mvc.Models.ViewModels;
using CasinoApp.Entities.TipoComida;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class CostoCasinoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public CostoCasinoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: CostoCasinoDtoes
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<CostoCasinoDto>>>("/api/CostoCasino");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);

                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: CostoCasinoDtoes/Details/5
        public  IActionResult  Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var empleadoResultado = client.Get<RequestResult<CostoCasinoDto>>($"/api/CostoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            if (empleadoResultado.IsSuccessful)
            {
                var costoCasino = empleadoResultado.Result;

                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);

                return View(costoCasino);
            }

            return NotFound();
        }

        // GET: CostoCasinoDtoes/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;

            var viewModel = new CUCostoCasinoViewModels
            {
                TipoComida = tipoComida,
                GruposEmpleado = gruposEmpleado,
            };

            return View(viewModel);
        }

        // POST: CostoCasinoDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public  IActionResult Create(CUCostoCasinoViewModels costoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<CostoCasinoDto>>("/api/CostoCasino", costoCasinoDto.CostoCasino);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: CostoCasinoDtoes/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<CostoCasinoDto>>($"/api/CostoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var grupoEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;

            if (resultado.IsSuccessful)
            {
                var viewModel = new CUCostoCasinoViewModels
                {
                    CostoCasino = resultado.Result,
                    TipoComida = tipoComida,
                    GruposEmpleado = grupoEmpleado
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // POST: CostoCasinoDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CUCostoCasinoViewModels costoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<CostoCasinoDto>>("/api/CostoCasino/Update", costoCasinoDto.CostoCasino);


            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: CostoCasinoDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CostoCasinoDto == null)
            {
                return NotFound();
            }

            var costoCasinoDto = await _context.CostoCasinoDto
                .FirstOrDefaultAsync(m => m.IdCostoCasino == id);
            if (costoCasinoDto == null)
            {
                return NotFound();
            }

            return View(costoCasinoDto);
        }

        // POST: CostoCasinoDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CostoCasinoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.CostoCasinoDto'  is null.");
            }
            var costoCasinoDto = await _context.CostoCasinoDto.FindAsync(id);
            if (costoCasinoDto != null)
            {
                _context.CostoCasinoDto.Remove(costoCasinoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostoCasinoDtoExists(int id)
        {
          return _context.CostoCasinoDto.Any(e => e.IdCostoCasino == id);
        }
    }
}
