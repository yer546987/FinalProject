using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;

namespace CasinoApp.Client.Mvc.Controllers.ParametroComida
{
    public class IngredientesController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public IngredientesController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: IngredientesDtoes
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<IngredientesDto>>>("/api/Ingredientes");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        // GET: IngredientesDtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IngredientesDto == null)
            {
                return NotFound();
            }

            var ingredientesDto = await _context.IngredientesDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientesDto == null)
            {
                return NotFound();
            }

            return View(ingredientesDto);
        }

        // GET: IngredientesDtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IngredientesDtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdTipoComida,Cantidad,IdProducto,NombreUnidadPesaje")] IngredientesDto ingredientesDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingredientesDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingredientesDto);
        }

        // GET: IngredientesDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IngredientesDto == null)
            {
                return NotFound();
            }

            var ingredientesDto = await _context.IngredientesDto.FindAsync(id);
            if (ingredientesDto == null)
            {
                return NotFound();
            }
            return View(ingredientesDto);
        }

        // POST: IngredientesDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdTipoComida,Cantidad,IdProducto,NombreUnidadPesaje")] IngredientesDto ingredientesDto)
        {
            if (id != ingredientesDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingredientesDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredientesDtoExists(ingredientesDto.Id))
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
            return View(ingredientesDto);
        }

        // GET: IngredientesDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IngredientesDto == null)
            {
                return NotFound();
            }

            var ingredientesDto = await _context.IngredientesDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingredientesDto == null)
            {
                return NotFound();
            }

            return View(ingredientesDto);
        }

        // POST: IngredientesDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IngredientesDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.IngredientesDto'  is null.");
            }
            var ingredientesDto = await _context.IngredientesDto.FindAsync(id);
            if (ingredientesDto != null)
            {
                _context.IngredientesDto.Remove(ingredientesDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredientesDtoExists(int id)
        {
          return _context.IngredientesDto.Any(e => e.Id == id);
        }
    }
}
