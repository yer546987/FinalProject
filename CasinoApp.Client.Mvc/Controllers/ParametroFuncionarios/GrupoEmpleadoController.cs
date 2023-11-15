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
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleadoControllers");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: GrupoEmpleado/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("Id,NombreGrupo")] GrupoEmpleadoDto grupoEmpleadoDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupoEmpleadoDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grupoEmpleadoDto);
        }

        // GET: GrupoEmpleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GrupoEmpleadoDto == null)
            {
                return NotFound();
            }

            var grupoEmpleadoDto = await _context.GrupoEmpleadoDto.FindAsync(id);
            if (grupoEmpleadoDto == null)
            {
                return NotFound();
            }
            return View(grupoEmpleadoDto);
        }

        // POST: GrupoEmpleado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreGrupo")] GrupoEmpleadoDto grupoEmpleadoDto)
        {
            if (id != grupoEmpleadoDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupoEmpleadoDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrupoEmpleadoDtoExists(grupoEmpleadoDto.Id))
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
            return View(grupoEmpleadoDto);
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
