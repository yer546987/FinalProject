using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Client.Mvc.Models.ViewModels;
using CasinoApp.Entities.TipoDocumento;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class TipoComidaController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public TipoComidaController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: TipoComida
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida");
            var ingredientes = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.Ingredientes = ingredientes.ToDictionary(x => x.Id, x => x.Cantidad);
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoComida/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var tipoComidaResult = client.Get<RequestResult<TipoComidaDto>>($"/api/TipoComida/{id}");
            var ingredientes = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes").Result;
            if (tipoComidaResult.IsSuccessful)
            {
                var tipocomida = tipoComidaResult.Result;

                ViewBag.Ingredientes = ingredientes.ToDictionary(x => x.Id, x => x.IdTipoComida);
                return View(tipocomida);
            }

            return NotFound();
        }

        // GET: TipoComida/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();
            var ingredientes = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes").Result;
            var viewModel = new CUTipoComidaViewModel
            {
                ingredientes = ingredientes
            };

            return View(viewModel);
        }

        // POST: TipoComida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CUTipoComidaViewModel tipoComidaViewModel)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<TipoComidaDto>>("/api/TipoComida", tipoComidaViewModel.tipoComida);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }


        // GET: TipoComida/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();

            var resultadoTipoComida = client.Get<RequestResult<TipoComidaDto>>($"/api/TipoComida/{id}");
            var ingredientes = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes").Result;

            if (resultadoTipoComida.IsSuccessful)
            {
                var viewModel = new CUTipoComidaViewModel
                {
                    tipoComida = resultadoTipoComida.Result,  
                    ingredientes = ingredientes
                };

                return View(viewModel);
            }

            return NotFound();
        }


        // POST: TipoComida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  Edit(CUTipoComidaViewModel tipoComidaDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<TipoComidaDto>>("/api/TipoComida/Update", tipoComidaDto.tipoComida);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: TipoComida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tipoComida == null)
            {
                return NotFound();
            }

            var tipoComidaDto = await _context.tipoComida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoComidaDto == null)
            {
                return NotFound();
            }

            return View(tipoComidaDto);
        }

        // POST: TipoComida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tipoComida == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.TipoComidaDto'  is null.");
            }
            var tipoComidaDto = await _context.tipoComida.FindAsync(id);
            if (tipoComidaDto != null)
            {
                _context.tipoComida.Remove(tipoComidaDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoComidaDtoExists(int id)
        {
          return _context.tipoComida.Any(e => e.Id == id);
        }
    }
}
