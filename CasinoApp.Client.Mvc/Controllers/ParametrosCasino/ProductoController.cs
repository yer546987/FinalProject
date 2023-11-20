using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Producto;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Client.Mvc.Models.ViewModels;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class ProductoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public ProductoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: Producto
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<ProductoDto>>>("/api/Producto");
            var tipoProductos = client.Get<RequestResult<List<TipoProductoDto>>>("/api/TipoProducto").Result;

            if (resultado.IsSuccessful)
            {
                var productoDtos = resultado.Result;

                ViewBag.TipoProducto = tipoProductos.ToDictionary(x => x.IdTipoProducto, x => x.TipoProducto);

                return View(productoDtos);
            }
            return NotFound();
        }

        // GET: Producto/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var productoResult = client.Get<RequestResult<ProductoDto>>($"/api/Producto/{id}");
            var tipoProductos = client.Get<RequestResult<List<TipoProductoDto>>>("/api/TipoProducto").Result;
            if (productoResult.IsSuccessful)
            {
                ViewBag.TipoProducto = tipoProductos.ToDictionary(x => x.IdTipoProducto, x => x.TipoProducto);
                return View(productoResult.Result);
            }

            return NotFound();
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var tipoProducto = client.Get<RequestResult<List<TipoProductoDto>>>("/api/TipoProducto").Result;

            var viewModel = new CuTipoProductoViewModel
            {
                TipoProducto = tipoProducto,

            };

            return View(viewModel);
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CuTipoProductoViewModel productoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<ProductoDto>>("/api/Producto", productoDto. producto);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: Producto/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<ProductoDto>>($"/api/Producto/{id}");
            var tipoProducto = client.Get<RequestResult<List<TipoProductoDto>>>("/api/TipoProducto").Result;

            if (resultado.IsSuccessful)
            {
                var viewModel = new CuTipoProductoViewModel
                {
                    producto = resultado.Result,
                    TipoProducto = tipoProducto
                };
                return View(viewModel);
            }

            return NotFound();
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CuTipoProductoViewModel productoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<ProductoDto>>("/api/Producto/Update", productoDto.producto);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductoDto == null)
            {
                return NotFound();
            }

            var productoDto = await _context.ProductoDto
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productoDto == null)
            {
                return NotFound();
            }

            return View(productoDto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.ProductoDto'  is null.");
            }
            var productoDto = await _context.ProductoDto.FindAsync(id);
            if (productoDto != null)
            {
                _context.ProductoDto.Remove(productoDto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoDtoExists(int id)
        {
            return _context.ProductoDto.Any(e => e.IdProducto == id);
        }
    }
}
