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
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoComida/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: TipoComida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoComida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,Descripcion,TiempoInicial,TiempoFinal,Limite,Cronograma")] TipoComidaDto tipoComidaDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoComidaDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoComidaDto);
        }

        // GET: TipoComida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tipoComida == null)
            {
                return NotFound();
            }

            var tipoComidaDto = await _context.tipoComida.FindAsync(id);
            if (tipoComidaDto == null)
            {
                return NotFound();
            }
            return View(tipoComidaDto);
        }

        // POST: TipoComida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,Descripcion,TiempoInicial,TiempoFinal,Limite,Cronograma")] TipoComidaDto tipoComidaDto)
        {
            if (id != tipoComidaDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoComidaDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoComidaDtoExists(tipoComidaDto.Id))
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
            return View(tipoComidaDto);
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
