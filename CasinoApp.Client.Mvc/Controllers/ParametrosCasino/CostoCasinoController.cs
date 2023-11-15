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
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<CostoCasinoDto>>>("/api/CostoCasino");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: CostoCasinoDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: CostoCasinoDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CostoCasinoDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCostoCasino,PrecioC,IdTipoComida,IdGrupoEmpleado,NombreGrupoEmpleado,NombreTipoComida")] CostoCasinoDto costoCasinoDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(costoCasinoDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(costoCasinoDto);
        }

        // GET: CostoCasinoDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CostoCasinoDto == null)
            {
                return NotFound();
            }

            var costoCasinoDto = await _context.CostoCasinoDto.FindAsync(id);
            if (costoCasinoDto == null)
            {
                return NotFound();
            }
            return View(costoCasinoDto);
        }

        // POST: CostoCasinoDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCostoCasino,PrecioC,IdTipoComida,IdGrupoEmpleado,NombreGrupoEmpleado,NombreTipoComida")] CostoCasinoDto costoCasinoDto)
        {
            if (id != costoCasinoDto.IdCostoCasino)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(costoCasinoDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostoCasinoDtoExists(costoCasinoDto.IdCostoCasino))
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
            return View(costoCasinoDto);
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
