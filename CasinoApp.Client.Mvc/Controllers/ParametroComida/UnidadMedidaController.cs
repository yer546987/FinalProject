using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.UnidadMedida;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.TipoDocumento;

namespace CasinoApp.Client.Mvc.Controllers.ParametroComida
{
    public class UnidadMedidaController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public UnidadMedidaController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: UnidadMedidaDtoes
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<UnidadMedidaDto>>>("/api/UnidadMedida");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: UnidadMedidaDtoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var unidadMedidaResult = client.Get<RequestResult<UnidadMedidaDto>>($"/api/UnidadMedida/{id}");
            if (unidadMedidaResult.IsSuccessful)
            {
                return View(unidadMedidaResult.Result);
            }

            return View(unidadMedidaResult);
        }

        // GET: UnidadMedidaDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadMedidaDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadMedidaDto unidadMedidaDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<UnidadMedidaDto>>("/api/UnidadMedida", unidadMedidaDto);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: UnidadMedidaDtoes/Edit/5
        public IActionResult  Edit(int? id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<UnidadMedidaDto>>($"/api/UnidadMedida/{id}");

            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }

            return NotFound();
        }

        // POST: UnidadMedidaDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UnidadMedidaDto unidadMedidaDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<UnidadMedidaDto>>("/api/UnidadMedida/Update", unidadMedidaDto);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: UnidadMedidaDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UnidadMedidaDto == null)
            {
                return NotFound();
            }

            var unidadMedidaDto = await _context.UnidadMedidaDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadMedidaDto == null)
            {
                return NotFound();
            }

            return View(unidadMedidaDto);
        }

        // POST: UnidadMedidaDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UnidadMedidaDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.UnidadMedidaDto'  is null.");
            }
            var unidadMedidaDto = await _context.UnidadMedidaDto.FindAsync(id);
            if (unidadMedidaDto != null)
            {
                _context.UnidadMedidaDto.Remove(unidadMedidaDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadMedidaDtoExists(int id)
        {
          return _context.UnidadMedidaDto.Any(e => e.Id == id);
        }
    }
}
