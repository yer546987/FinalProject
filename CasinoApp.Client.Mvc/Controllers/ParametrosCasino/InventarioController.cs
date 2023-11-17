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
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<InventarioDto>>>("/api/InventarioControllers");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: Inventario/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Inventario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<IActionResult> Create([Bind("Id,Producto,FechaVencimiento,Stock,IdUnidadMedida,Cantidad,Mecatos,IdInventario")] InventarioDto inventarioDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventarioDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventarioDto);
        }

        // GET: Inventario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InventarioDto == null)
            {
                return NotFound();
            }

            var inventarioDto = await _context.InventarioDto.FindAsync(id);
            if (inventarioDto == null)
            {
                return NotFound();
            }
            return View(inventarioDto);
        }

        // POST: Inventario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Producto,FechaVencimiento,Stock,IdUnidadMedida,Cantidad,Mecatos,IdInventario")] InventarioDto inventarioDto)
        {
            if (id != inventarioDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventarioDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioDtoExists(inventarioDto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inventarioDto);
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
