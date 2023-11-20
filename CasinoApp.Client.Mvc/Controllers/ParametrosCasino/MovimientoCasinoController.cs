using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Client.Mvc.Models.ViewModels;
using Azure;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using System.Net.Http;
using System.Net;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosCasino
{
    public class MovimientoCasinoController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public MovimientoCasinoController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        // GET: MovimientoCasino
        public async Task<IActionResult> Index()
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<List<MovimientoCasinoDto>>>("/api/MovimientoCasino");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (resultado.IsSuccessful)
            {
                ViewBag.TiposEmpleados = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GruposEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.Empleado = Empleado.ToDictionary(x => x.IdEmpleado, x => x.NombreEmpleado);

                return View(resultado.Result);
            }
            return NotFound();
        }

        public IActionResult Export()
        {
            try
            {
                MVAHttpClient client = new MVAHttpClient();
                var resultado = client.Get<RequestResult<List<MovimientoCasinoDto>>>("/api/MovimientoCasino");
                var tipoComidaResult = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida");
                var gruposEmpleadoResult = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado");
                var empleadoResult = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado");

                if (resultado.IsSuccessful && tipoComidaResult.IsSuccessful && gruposEmpleadoResult.IsSuccessful && empleadoResult.IsSuccessful)
                {
                    var tipoComida = tipoComidaResult.Result.ToDictionary(x => x.Id, x => x.Nombre);
                    var gruposEmpleado = gruposEmpleadoResult.Result.ToDictionary(x => x.Id, x => x.NombreGrupo);
                    var empleado = empleadoResult.Result.ToDictionary(x => x.IdEmpleado, x => $"{x.NombreEmpleado} {x.ApellidoE}");

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("MovimientosCasino");

                        // Añadir encabezado
                        AddHeader(worksheet);

                        int rowIndex = 2;

                        foreach (var movimiento in resultado.Result)
                        {
                            worksheet.Row(rowIndex).Cell(1).Value = movimiento.HoraRegistro;
                            worksheet.Row(rowIndex).Cell(2).Value = movimiento.IdEmpleado;
                            worksheet.Row(rowIndex).Cell(3).Value = empleado.GetValueOrDefault(movimiento.IdEmpleado, "Nombre No Encontrado");
                            worksheet.Row(rowIndex).Cell(4).Value = gruposEmpleado.GetValueOrDefault(movimiento.IdGrupoEmpleado, "Grupo No Encontrado");
                            worksheet.Row(rowIndex).Cell(5).Value = tipoComida.GetValueOrDefault(movimiento.IdTipoComida, "Tipo de Comida No Encontrado");
                            worksheet.Row(rowIndex).Cell(6).Value = movimiento.Costo;
                            worksheet.Row(rowIndex).Cell(1).Style.NumberFormat.Format = "yyyy-MM-dd HH:mm";
                            worksheet.Row(rowIndex).Cell(6).Style.NumberFormat.Format = "$#,##0.00";
                            rowIndex++;
                        }

                        // Aplicar estilos a las celdas de datos
                        var dataRange = worksheet.Range(worksheet.Cell(2, 1), worksheet.Cell(rowIndex <= 2 ? 2 : rowIndex - 1, 6));
                        dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        dataRange.Style.Fill.BackgroundColor = XLColor.White;
                        dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        dataRange.Style.Border.OutsideBorderColor = XLColor.Gray;
                        dataRange.Style.Border.InsideBorderColor = XLColor.Gray;

                        // Ajustar el ancho de las columnas
                        for (int i = 1; i <= 6; i++)
                        {
                            worksheet.Column(i).AdjustToContents();
                        }

                        // Aplicar bordes
                        var allRange = worksheet.Range(worksheet.Cell(1, 1), worksheet.Cell(rowIndex - 1, 6));
                        allRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        allRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                        // Guardar en MemoryStream
                        using (var memoryStream = new MemoryStream())
                        {
                            workbook.SaveAs(memoryStream);
                            memoryStream.Position = 0;

                            // Configurar respuesta HTTP
                            var result = new HttpResponseMessage(HttpStatusCode.OK)
                            {
                                Content = new ByteArrayContent(memoryStream.ToArray())
                            };

                            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                            {
                                FileName = "MovimientosCasino.xlsx"
                            };
                            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                            // Convertir la respuesta HTTP en un ActionResult
                            var responseResult = new FileContentResult(result.Content.ReadAsByteArrayAsync().Result, result.Content.Headers.ContentType.ToString())
                            {
                                FileDownloadName = result.Content.Headers.ContentDisposition.FileName
                            };

                            return responseResult;
                        }
                    }
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al exportar a Excel: {ex.Message}");
                return BadRequest($"Error al exportar a Excel: {ex.Message}");
            }
        }

        private static void AddHeader(IXLWorksheet worksheet)
        {
            worksheet.Row(1).Cell(1).Value = "Fecha";
            worksheet.Row(1).Cell(2).Value = "Identificación";
            worksheet.Row(1).Cell(3).Value = "Nombre de funcionario";
            worksheet.Row(1).Cell(4).Value = "Grupo de funcionario";
            worksheet.Row(1).Cell(5).Value = "Alimento";
            worksheet.Row(1).Cell(6).Value = "Valor($)";

            // Estilos de encabezado
            worksheet.Row(1).Style.Font.Bold = true;
            worksheet.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
            worksheet.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }





        // GET: MovimientoCasino/Details/5
        public IActionResult Details(int id)
        {

            MVAHttpClient client = new MVAHttpClient();
            var movimientoCasinoResult = client.Get<RequestResult<MovimientoCasinoDto>>($"/api/MovimientoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (movimientoCasinoResult.IsSuccessful)
            {
                var movimientoCasino = movimientoCasinoResult.Result;

                ViewBag.TipoComida = tipoComida.ToDictionary(x => x.Id, x => x.Nombre);
                ViewBag.GrupoEmpleado = gruposEmpleado.ToDictionary(x => x.Id, x => x.NombreGrupo);
                ViewBag.Empleado = Empleado.ToDictionary(x => x.IdEmpleado, x => x.NombreEmpleado);
                return View(movimientoCasino);
            }

            return NotFound();
        }

        // GET: MovimientoCasino/Create
        public IActionResult Create()
        {
            MVAHttpClient client = new MVAHttpClient();

            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;

            var viewModel = new CUMovimientoCasinoViewModel
            {
                grupoEmpleados = gruposEmpleado,
                empleado = Empleado,
                tipoComida = tipoComida
            };

            return View(viewModel);
        }

        // POST: MovimientoCasino/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(CUMovimientoCasinoViewModel movimientoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<MovimientoCasinoDto>>("/api/MovimientoCasino", movimientoCasinoDto.movimientoCasino);
            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index", resultado.Result);
            }
            return NotFound();
        }

        // GET: MovimientoCasino/Edit/5
        public IActionResult Edit(int id)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Get<RequestResult<MovimientoCasinoDto>>($"/api/MovimientoCasino/{id}");
            var tipoComida = client.Get<RequestResult<List<TipoComidaDto>>>("/api/TipoComida").Result;
            var gruposEmpleado = client.Get<RequestResult<List<GrupoEmpleadoDto>>>("/api/GrupoEmpleado").Result;
            var Empleado = client.Get<RequestResult<List<EmpleadoDto>>>("/api/Empleado").Result;
            if (resultado.IsSuccessful)
            {
                var viewModel = new CUMovimientoCasinoViewModel
                {
                    movimientoCasino = resultado.Result,
                    tipoComida = tipoComida,
                    grupoEmpleados = gruposEmpleado,
                    empleado = Empleado
                };

                return View(viewModel);
            }

            return NotFound();
        }

        // POST: MovimientoCasino/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(CUMovimientoCasinoViewModel movimientoCasinoDto)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<MovimientoCasinoDto>>("/api/MovimientoCasino/Update", movimientoCasinoDto.movimientoCasino);

            if (resultado.IsSuccessful)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        // GET: MovimientoCasino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovimientoCasinoDto == null)
            {
                return NotFound();
            }

            var movimientoCasinoDto = await _context.MovimientoCasinoDto
                .FirstOrDefaultAsync(m => m.IdMovimientoCasino == id);
            if (movimientoCasinoDto == null)
            {
                return NotFound();
            }

            return View(movimientoCasinoDto);
        }

        // POST: MovimientoCasino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovimientoCasinoDto == null)
            {
                return Problem("Entity set 'CasinoAppClientMvcContext.MovimientoCasinoDto'  is null.");
            }
            var movimientoCasinoDto = await _context.MovimientoCasinoDto.FindAsync(id);
            if (movimientoCasinoDto != null)
            {
                _context.MovimientoCasinoDto.Remove(movimientoCasinoDto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimientoCasinoDtoExists(int id)
        {
            return _context.MovimientoCasinoDto.Any(e => e.IdMovimientoCasino == id);
        }
    }
}
