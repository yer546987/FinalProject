using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;

namespace CasinoApp.Client.Mvc.Controllers.ParametroFuncionarios
{
    public class GrupoEmpleadoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public GrupoEmpleadoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: GrupoEmpleado
        public IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado");

            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: GrupoEmpleado/Details/5
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var grupoEmpleadoResultado = client.Get<RequestResult<GrupoEmpleadoDto>>($"/api/GrupoEmpleado/{id}");
            if (grupoEmpleadoResultado.IsSuccessful)
            {
                return View(grupoEmpleadoResultado.Result);
            }

            return NotFound();
        }

        // GET: GrupoEmpleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GrupoEmpleado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GrupoEmpleadoDto grupoEmpleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<GrupoEmpleadoDto>>("/api/GrupoEmpleado", grupoEmpleadoDto);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }


        // GET: GrupoEmpleado/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<GrupoEmpleadoDto>>($"/api/GrupoEmpleado/{id}");

            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }

            return NotFound();
        }

        // POST: GrupoEmpleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(GrupoEmpleadoDto grupoEmpleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<GrupoEmpleadoDto>>("/api/GrupoEmpleado/Update", grupoEmpleadoDto);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }



        // GET: GrupoEmpleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GrupoEmpleadoDto == null)
            {
                return NotFound();
            }

            var grupoEmpleadoDto = await _context.GrupoEmpleadoDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupoEmpleadoDto == null)
            {
                return NotFound();
            }

            return View(grupoEmpleadoDto);
        }

        // POST: GrupoEmpleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GrupoEmpleadoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.GrupoEmpleadoDto'  is null.");
            }
            var grupoEmpleadoDto = await _context.GrupoEmpleadoDto.FindAsync(id);
            if (grupoEmpleadoDto != null)
            {
                _context.GrupoEmpleadoDto.Remove(grupoEmpleadoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrupoEmpleadoDtoExists(int id)
        {
          return _context.GrupoEmpleadoDto.Any(e => e.Id == id);
        }
    }
}
