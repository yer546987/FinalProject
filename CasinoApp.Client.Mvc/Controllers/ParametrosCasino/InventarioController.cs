using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Inventario;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Client.Mvc.Models.ViewModels;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class InventarioController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public InventarioController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: Inventario
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<InventarioDto>>>("/api/Inventario");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.Producto = producto.ToDictionary(x => x.IdProducto, x => x.NombreProducto);

                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: Inventario/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var requestResult = client.Get<RequestResult<InventarioDto>>($"/api/Inventario/{id}");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;
            if (requestResult.IsSuccessful)
            {
                var ingredientes = requestResult.Result;

                ViewBag.Producto = producto.ToDictionary(x => x.IdProducto, x => x.NombreProducto);
                return View(ingredientes);
            }

            return NotFound();
        }

        // GET: Inventario/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;

            var viewModel = new CuInventarioViewModel
            {
                Producto = producto
            };

            return View(viewModel);
        }

        // POST: Inventario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CuInventarioViewModel inventarioDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<InventarioDto>>("/api/Inventario", inventarioDto.Inventario);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: Inventario/Edit/5
        public IActionResult Edit(int? id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<InventarioDto>>($"/api/Inventario/{id}");
            var producto = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto").Result;

            if (resultado.IsSuccessful)
            {
                var viewModel = new CuInventarioViewModel
                {
                    Inventario = resultado.Result,
                    Producto = producto,
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // POST: Inventario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CuInventarioViewModel inventarioDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<InventarioDto>>("/api/Inventario/Update", inventarioDto.Inventario);


            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: Inventario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InventarioDto == null)
            {
                return NotFound();
            }

            var inventarioDto = await _context.InventarioDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventarioDto == null)
            {
                return NotFound();
            }

            return View(inventarioDto);
        }

        // POST: Inventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InventarioDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.InventarioDto'  is null.");
            }
            var inventarioDto = await _context.InventarioDto.FindAsync(id);
            if (inventarioDto != null)
            {
                _context.InventarioDto.Remove(inventarioDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioDtoExists(int id)
        {
          return _context.InventarioDto.Any(e => e.Id == id);
        }
    }
}
