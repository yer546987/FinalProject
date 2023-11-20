using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Client.Mvc.Models.ViewModels;

namespace CasinoApp.Client.Mvc.Controllers.ParametroComida
{
    public class IngredientesController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public IngredientesController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: IngredientesDtoes
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.Producto = producto.ToDictionary(x => x.IdProducto, x => x.NombreProducto);
                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);

                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: IngredientesDtoes/Details/5
        public  IActionResult  Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var requestResult = client.Get<RequestResult<IngredientesDto>>($"/api/Ingredientes/{id}");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            if (requestResult.IsSuccessful)
            {
                var ingredientes = requestResult.Result;

                ViewBag.Producto = producto.ToDictionary(x => x.IdProducto, x => x.NombreProducto);
                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                return View(ingredientes);
            }

            return NotFound();
        }

        // GET: IngredientesDtoes/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;

            var viewModel = new CuIngredientesViewModel
            {
                Productos = producto,
                TipoComidas = tipoComida
            };

            return View(viewModel);
        }

        // POST: IngredientesDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CuIngredientesViewModel ingredientesDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<IngredientesDto>>("/api/Ingredientes", ingredientesDto.Ingredientes);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: IngredientesDtoes/Edit/5
        public  IActionResult  Edit(int? id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<IngredientesDto>>($"/api/Ingredientes/{id}");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;

            if (resultado.IsSuccessful)
            {
                var viewModel = new CuIngredientesViewModel
                {
                    Ingredientes = resultado.Result,
                    Productos = producto,
                    TipoComidas = tipoComida,
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // POST: IngredientesDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CuIngredientesViewModel ingredientesDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<IngredientesDto>>("/api/Ingredientes/Update", ingredientesDto.Ingredientes);


            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: IngredientesDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IngredientesDto == null)
            {
                return NotFound();
            }

            var ingredientesDto = await _context.IngredientesDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientesDto == null)
            {
                return NotFound();
            }

            return View(ingredientesDto);
        }

        // POST: IngredientesDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IngredientesDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.IngredientesDto'  is null.");
            }
            var ingredientesDto = await _context.IngredientesDto.FindAsync(id);
            if (ingredientesDto != null)
            {
                _context.IngredientesDto.Remove(ingredientesDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientesDtoExists(int id)
        {
          return _context.IngredientesDto.Any(e => e.Id == id);
        }
    }
}
