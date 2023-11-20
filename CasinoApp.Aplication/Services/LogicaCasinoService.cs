using Azure.Core.Diagnostics;
using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class LogicaCasinoService : ILogicaCasinoService
    {
        private CasinoAppContext _Context;

        public LogicaCasinoService()
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<MovimientoCasinoDto> Create(int Identificacion, int idTipocomida)
        {
            try
            {
                if (Identificacion == 0)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Tiene que ingresar una identificación");
                if (idTipocomida == 0)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Tiene que ingresar un tipo de comida");

                var empleado = _Context.Empleados.FirstOrDefault(x => x.Identificacion == Identificacion);
                var tipoComida = _Context.TipoComida.FirstOrDefault(x => x.Id == idTipocomida);

                if (empleado == null || tipoComida == null)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Empleado o tipo de comida no encontrados");

                var costo = _Context.CostoCasinos.FirstOrDefault(x => x.IdGrupoEmpleado == empleado.IdGrupoE);
              
                // Verificar límite de pedidos por día
                var pedidosHoy = _Context.MovimientoCasinos
                    .Where(x => x.IdEmpleado == empleado.Id && x.IdTipoComida == tipoComida.Id
                                && x.HoraRegistro.Date == DateTime.Now.Date)
                    .Count();

                if (pedidosHoy >= tipoComida.Limite)
                {
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess($"El empleado ha alcanzado el límite de pedidos para el tipo de comida {tipoComida.Nombre} hoy.");
                }

                MovimientoCasino entity = new MovimientoCasino();
                entity.HoraRegistro = DateTime.Now;
                if (costo == null)
                { entity.Costo = tipoComida.Precio; }
                else 
                { entity.Costo = costo.Precio; }  
                entity.IdTipoComida = tipoComida.Id;
                entity.IdGrupoEmpleado = empleado.IdGrupoE;
                entity.IdEmpleado = empleado.Id;
                var result = _Context.MovimientoCasinos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows == 0)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el movimiento de casino.");

                var resultado = new MovimientoCasinoDto()
                {
                    IdMovimientoCasino = result.Entity.Id,
                    Costo = result.Entity.Costo,
                    HoraRegistro = result.Entity.HoraRegistro,
                    IdEmpleado = result.Entity.IdEmpleado,
                    IdTipoComida = result.Entity.IdTipoComida,
                    IdGrupoEmpleado = result.Entity.IdGrupoEmpleado
                };

                return RequestResult<MovimientoCasinoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<MovimientoCasinoDto>.CreateError(ex.Message);
            }
        }


        public RequestResult<List<TipoComidaDto>> GetAll()
        {
            try
            {
                DateTime currentTime = DateTime.Now;

                var comidas = _Context.TipoComida
                    .Where(x => (x.Cronograma && x.TiempoInicial <= currentTime && x.TiempoFinal >= currentTime)
                                || (!x.Cronograma))
                    .ToList();
                List<TipoComidaDto> result = new List<TipoComidaDto>();
                foreach (var item in comidas)
                {
                    result.Add(new TipoComidaDto()
                    {
                        Id= item.Id,
                        Descripcion = item.Descripcion,
                        Cronograma = item.Cronograma,
                        TiempoFinal = item.TiempoFinal,
                        TiempoInicial = item.TiempoInicial,
                        Limite = item.Limite,
                        Nombre = item.Nombre,
                        Precio = item.Precio
                        
                    });
                }

                return RequestResult<List<TipoComidaDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public RequestResult<EmpleadoDto> GetById(int identificacionE)
        {
            try
            {
                var empleado = _Context.Empleados.Where(x => x.Identificacion == identificacionE).FirstOrDefault();
                if (empleado is null) return RequestResult<EmpleadoDto>.CreateNoSuccess($"No existe el empleado con identificador {identificacionE}");
                var resultado = new EmpleadoDto()
                {
                    NombreEmpleado = empleado.Nombre,
                    ApellidoE = empleado.Apellido,
                    IdEmpleado = empleado.Id,
                    IdentificacionE = (int)empleado.Identificacion,
                    IdGrupoEE = empleado.IdGrupoE,
                    IdTipoEmpleadoE = empleado.IdTipoEmpleado,
                    IdTipoIdentificacionE = empleado.IdTipoIdentificacion,
                    InternoE = empleado.Interno
                };
                return RequestResult<EmpleadoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<EmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }
    }
}
