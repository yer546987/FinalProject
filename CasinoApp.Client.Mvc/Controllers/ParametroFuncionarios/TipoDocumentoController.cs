using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;

namespace CasinoApp.Client.Mvc.Controllers.ParametroFuncionarios
{
    public class TipoDocumentoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public TipoDocumentoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: TipoDocumento
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<TipoDocumentoDto>>>("/api/TipoDocumentoControllers");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoDocumento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoDocumentoDto == null)
            {
                return NotFound();
            }

            var tipoDocumentoDto = await _context.TipoDocumentoDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDocumentoDto == null)
            {
                return NotFound();
            }

            return View(tipoDocumentoDto);
        }

        // GET: TipoDocumento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDocumento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoIdentificacion")] TipoDocumentoDto tipoDocumentoDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDocumentoDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDocumentoDto);
        }

        // GET: TipoDocumento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoDocumentoDto == null)
            {
                return NotFound();
            }

            var tipoDocumentoDto = await _context.TipoDocumentoDto.FindAsync(id);
            if (tipoDocumentoDto == null)
            {
                return NotFound();
            }
            return View(tipoDocumentoDto);
        }

        // POST: TipoDocumento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoIdentificacion")] TipoDocumentoDto tipoDocumentoDto)
        {
            if (id != tipoDocumentoDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDocumentoDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDocumentoDtoExists(tipoDocumentoDto.Id))
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
            return View(tipoDocumentoDto);
        }

        // GET: TipoDocumento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoDocumentoDto == null)
            {
                return NotFound();
            }

            var tipoDocumentoDto = await _context.TipoDocumentoDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoDocumentoDto == null)
            {
                return NotFound();
            }

            return View(tipoDocumentoDto);
        }

        // POST: TipoDocumento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoDocumentoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.TipoDocumentoDto'  is null.");
            }
            var tipoDocumentoDto = await _context.TipoDocumentoDto.FindAsync(id);
            if (tipoDocumentoDto != null)
            {
                _context.TipoDocumentoDto.Remove(tipoDocumentoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDocumentoDtoExists(int id)
        {
          return _context.TipoDocumentoDto.Any(e => e.Id == id);
        }
    }
}
