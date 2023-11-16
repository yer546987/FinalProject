using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;

namespace CasinoApp.Client.Mvc.Controllers.ParametroFuncionarios
{
    public class TipoEmpleadoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public TipoEmpleadoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: TipoEmpleado
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<TipoEmpleadoDto>>>("/api/TipoEmpleado");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoEmpleado/Details/5
        public  IActionResult  Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var tipoEmpleadoResult = client.Get<RequestResult<TipoEmpleadoDto>>($"/api/TipoEmpleado/{id}");
            if (tipoEmpleadoResult.IsSuccessful)
            {
                return View(tipoEmpleadoResult.Result);
            }

            return View(tipoEmpleadoResult);
        }

        // GET: TipoEmpleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEmpleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Create(TipoEmpleadoDto tipoEmpleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<TipoEmpleadoDto>>("/api/TipoEmpleado", tipoEmpleadoDto);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: TipoEmpleado/Edit/5
        public  IActionResult  Edit(int? id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<TipoEmpleadoDto>>($"/api/TipoEmpleado/{id}");

            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }

            return NotFound();
        }

        // POST: TipoEmpleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Edit(TipoEmpleadoDto tipoEmpleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<TipoEmpleadoDto>>("/api/TipoEmpleado/Update", tipoEmpleadoDto);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: TipoEmpleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEmpleadoDto == null)
            {
                return NotFound();
            }

            var tipoEmpleadoDto = await _context.TipoEmpleadoDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEmpleadoDto == null)
            {
                return NotFound();
            }

            return View(tipoEmpleadoDto);
        }

        // POST: TipoEmpleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEmpleadoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.TipoEmpleadoDto'  is null.");
            }
            var tipoEmpleadoDto = await _context.TipoEmpleadoDto.FindAsync(id);
            if (tipoEmpleadoDto != null)
            {
                _context.TipoEmpleadoDto.Remove(tipoEmpleadoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEmpleadoDtoExists(int id)
        {
          return _context.TipoEmpleadoDto.Any(e => e.Id == id);
        }
    }
}
