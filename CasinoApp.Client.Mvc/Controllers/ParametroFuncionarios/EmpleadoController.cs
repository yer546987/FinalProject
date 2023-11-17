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
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Client.Mvc.Models.ViewModels;

namespace CasinoApp.Client.Mvc.Controllers.ParametroFuncionarios
{
    public class EmpleadoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public EmpleadoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: EmpleadoDtoes
        public  IActionResult Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado");
            var tiposEmpleados = client.Get<RequestResult<List<TipoEmpleadoDto>>>("/api/TipoEmpleado").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var tiposIdentificacion = client.Get<RequestResult<List<TipoDocumentoDto>>>("/api/TipoDocumento").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.TiposEmpleados = tiposEmpleados.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.TiposIdentificacion = tiposIdentificacion.ToDictionary(x => x.Id, x => x.TipoIdentificacion);

                return View(resultado.Result);
            }

            return NotFound();
        }

        // GET: EmpleadoDtoes/Details/5
        // GET: EmpleadoDtoes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MVAHttpClient client = new MVAHttpClient();
            var empleadoResultado = client.Get<RequestResult<EmpleadoDto>>($"/api/Empleado/{id}");
            var tiposEmpleados = client.Get<RequestResult<List<TipoEmpleadoDto>>>("/api/TipoEmpleado").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var tiposIdentificacion = client.Get<RequestResult<List<TipoDocumentoDto>>>("/api/TipoDocumento").Result;
            if (empleadoResultado.IsSuccessful)
            {
                var empleado = empleadoResultado.Result;

                ViewBag.TiposEmpleados = tiposEmpleados.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.TiposIdentificacion = tiposIdentificacion.ToDictionary(x => x.Id, x => x.TipoIdentificacion);
                return View(empleado);
            }

            return NotFound();
        }


        // GET: EmpleadoDtoes/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var tiposEmpleados = client.Get<RequestResult<List<TipoEmpleadoDto>>>("/api/TipoEmpleado").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var tiposIdentificacion = client.Get<RequestResult<List<TipoDocumentoDto>>>("/api/TipoDocumento").Result;

            var viewModel = new CUEmpledoViewModel
            {
                TiposEmpleado = tiposEmpleados,
                GruposEmpleado = gruposEmpleado,
                TiposIdentificacion = tiposIdentificacion
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Create(CUEmpledoViewModel empleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<EmpleadoDto>>("/api/Empleado", empleadoDto.Empleado);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index",resultado.Result);
            }
            return NotFound();
        }

        // GET: EmpleadoDtoes/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<EmpleadoDto>>($"/api/Empleado/{id}");
            var tiposEmpleados = client.Get<RequestResult<List<TipoEmpleadoDto>>>("/api/TipoEmpleado").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var tiposIdentificacion = client.Get<RequestResult<List<TipoDocumentoDto>>>("/api/TipoDocumento").Result;

            if (resultado.IsSuccessful)
            {
                var viewModel = new CUEmpledoViewModel
                {
                    Empleado = resultado.Result,
                    TiposEmpleado = tiposEmpleados,
                    GruposEmpleado = gruposEmpleado,
                    TiposIdentificacion = tiposIdentificacion
                };

                return View(viewModel);
            }

            return NotFound();
        }



        [HttpPost]
        public IActionResult Edit(CUEmpledoViewModel empleadoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<EmpleadoDto>>("/api/Empleado/Update", empleadoDto.Empleado);


            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index"); 
            }

            return NotFound(); 
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
