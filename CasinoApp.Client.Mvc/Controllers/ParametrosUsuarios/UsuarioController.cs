﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Usuario;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosUsuarios
{
    public class UsuarioController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public UsuarioController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<UsuarioDto>>>("/api/Usuario");
            if (resultado.IsSuccessful)
            {
                return View(resultado.Result);
            }
            return NotFound();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuarioDto == null)
            {
                return NotFound();
            }

            var usuarioDto = await _context.UsuarioDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioDto == null)
            {
                return NotFound();
            }

            return View(usuarioDto);
        }

        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Usuario1")] UsuarioDto usuarioDto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioDto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioDto);
        }

        // GET: UsuarioDtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuarioDto == null)
            {
                return NotFound();
            }

            var usuarioDto = await _context.UsuarioDto.FindAsync(id);
            if (usuarioDto == null)
            {
                return NotFound();
            }
            return View(usuarioDto);
        }

        // POST: UsuarioDtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Usuario1")] UsuarioDto usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioDto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioDtoExists(usuarioDto.Id))
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
            return View(usuarioDto);
        }

        // GET: UsuarioDtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuarioDto == null)
            {
                return NotFound();
            }

            var usuarioDto = await _context.UsuarioDto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioDto == null)
            {
                return NotFound();
            }

            return View(usuarioDto);
        }

        // POST: UsuarioDtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuarioDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.UsuarioDto'  is null.");
            }
            var usuarioDto = await _context.UsuarioDto.FindAsync(id);
            if (usuarioDto != null)
            {
                _context.UsuarioDto.Remove(usuarioDto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioDtoExists(int id)
        {
          return _context.UsuarioDto.Any(e => e.Id == id);
        }
    }
}
