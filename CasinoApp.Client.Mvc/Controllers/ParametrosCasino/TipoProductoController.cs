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
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class TipoProductoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public TipoProductoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: TipoProducto
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<TipoProductoDto>>>("/api/TipoProducto");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoProducto/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var tipoProductoResult = client.Get<RequestResult<TipoProductoDto>>($"/api/TipoProducto/{id}");
            if (tipoProductoResult.IsSuccessful)
            {
                return View(tipoProductoResult.Result);
            }

            return NotFound();
        }

        // GET: TipoProducto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProducto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(TipoProductoDto tipoProductoDto)
        {
            {
                MVAHttpClient client = new MVAHttpClient();
                var resultado = client.Post<RequestResult<TipoProductoDto>>("/api/TipoProducto", tipoProductoDto);
                if (resultado.IsSuccessful)
                {
                    return RedirectToAction("Index", resultado.Result);
                }
                return NotFound();
            }
        }

        // GET: TipoProducto/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<TipoProductoDto>>($"/api/TipoProducto/{id}");

            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }

            return NotFound();
        }

        // POST: TipoProducto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(TipoProductoDto tipoProductoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<TipoProductoDto>>("/api/TipoProducto/Update", tipoProductoDto);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: TipoProducto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoProductoDto == null)
            {
                return NotFound();
            }

            var tipoProductoDto = await _context.TipoProductoDto
                .FirstOrDefaultAsync(m => m.IdTipoProducto == id);
            if (tipoProductoDto == null)
            {
                return NotFound();
            }

            return View(tipoProductoDto);
        }

        // POST: TipoProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoProductoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.TipoProductoDto'  is null.");
            }
            var tipoProductoDto = await _context.TipoProductoDto.FindAsync(id);
            if (tipoProductoDto != null)
            {
                _context.TipoProductoDto.Remove(tipoProductoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProductoDtoExists(int id)
        {
          return _context.TipoProductoDto.Any(e => e.IdTipoProducto == id);
        }
    }
}
