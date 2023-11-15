using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Empleado;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;

namespace CasinoApp.Client.Mvc.Controllers.ParametroFuncionarios
{
    public class EmpleadoDtoesController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public EmpleadoDtoesController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: EmpleadoDtoes
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: EmpleadoDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmpleadoDto == null)
            {
                return NotFound();
            }

            var empleadoDto = await _context.EmpleadoDto
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleadoDto == null)
            {
                return NotFound();
            }

            return View(empleadoDto);
        }

        // GET: EmpleadoDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmpleadoDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,NombreEmpleado,ApellidoE,IdTipoIdentificacionE,IdentificacionE,IdTipoEmpleadoE,IdGrupoEE,InternoE,NombreGrupoEmpleadoE,NombreTipoEmpleadoE,NombreTipoIdentificacionE")] EmpleadoDto empleadoDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleadoDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleadoDto);
        }

        // GET: EmpleadoDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmpleadoDto == null)
            {
                return NotFound();
            }

            var empleadoDto = await _context.EmpleadoDto.FindAsync(id);
            if (empleadoDto == null)
            {
                return NotFound();
            }
            return View(empleadoDto);
        }

        // POST: EmpleadoDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,NombreEmpleado,ApellidoE,IdTipoIdentificacionE,IdentificacionE,IdTipoEmpleadoE,IdGrupoEE,InternoE,NombreGrupoEmpleadoE,NombreTipoEmpleadoE,NombreTipoIdentificacionE")] EmpleadoDto empleadoDto)
        {
            if (id != empleadoDto.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleadoDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoDtoExists(empleadoDto.IdEmpleado))
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
            return View(empleadoDto);
        }

        // GET: EmpleadoDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmpleadoDto == null)
            {
                return NotFound();
            }

            var empleadoDto = await _context.EmpleadoDto
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleadoDto == null)
            {
                return NotFound();
            }

            return View(empleadoDto);
        }

        // POST: EmpleadoDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmpleadoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.EmpleadoDto'  is null.");
            }
            var empleadoDto = await _context.EmpleadoDto.FindAsync(id);
            if (empleadoDto != null)
            {
                _context.EmpleadoDto.Remove(empleadoDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoDtoExists(int id)
        {
          return _context.EmpleadoDto.Any(e => e.IdEmpleado == id);
        }
    }
}
