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
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: MovimientoCasino/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: MovimientoCasino/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovimientoCasino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovimientoCasino,Costo,IdTipoComida,IdGrupoEmpleado,HoraRegistro,IdEmpleado,NombreEmpleado,NombreGrupoEmpleado,NombreTipoComida")] MovimientoCasinoDto movimientoCasinoDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimientoCasinoDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movimientoCasinoDto);
        }

        // GET: MovimientoCasino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovimientoCasinoDto == null)
            {
                return NotFound();
            }

            var movimientoCasinoDto = await _context.MovimientoCasinoDto.FindAsync(id);
            if (movimientoCasinoDto == null)
            {
                return NotFound();
            }
            return View(movimientoCasinoDto);
        }

        // POST: MovimientoCasino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMovimientoCasino,Costo,IdTipoComida,IdGrupoEmpleado,HoraRegistro,IdEmpleado,NombreEmpleado,NombreGrupoEmpleado,NombreTipoComida")] MovimientoCasinoDto movimientoCasinoDto)
        {
            if (id != movimientoCasinoDto.IdMovimientoCasino)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimientoCasinoDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimientoCasinoDtoExists(movimientoCasinoDto.IdMovimientoCasino))
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
            return View(movimientoCasinoDto);
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
