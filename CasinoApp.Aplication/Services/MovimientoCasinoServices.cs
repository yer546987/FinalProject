using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class MovimientoCasinoServices : IMovimientoCasinoServices
    {

        private CasinoAppContext _Context;

        public MovimientoCasinoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<MovimientoCasinoDto> Create(MovimientoCasinoDto movimientoCasino)
        {
            try
            {
                if (movimientoCasino is null)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Los datos son requeridos");
                MovimientoCasino entity = new MovimientoCasino();
                entity.HoraRegistro = movimientoCasino.HoraRegistro;
                entity.Costo = movimientoCasino.Costo;
                entity.IdTipoComida = movimientoCasino.IdTipoComida;
                entity.IdGrupoEmpleado = movimientoCasino.IdGrupoEmpleado;
                entity.IdEmpleado = movimientoCasino.IdEmpleado;
                var result = _Context.MovimientoCasinos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
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

        public bool Delete(int idMovimientoCasino)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<MovimientoCasinoDto>> GetAll()
        {
            try
            {
                var movimientoCasino = _Context.MovimientoCasinos.ToList();
                List<MovimientoCasinoDto> result = new List<MovimientoCasinoDto>();
                foreach (var item in movimientoCasino)
                {
                    result.Add(new MovimientoCasinoDto()
                    {
                        IdMovimientoCasino = item.Id,
                        Costo = item.Costo,
                        HoraRegistro = item.HoraRegistro,
                        IdEmpleado = item.IdEmpleado,
                        IdTipoComida = item.IdTipoComida,
                        IdGrupoEmpleado = item.IdGrupoEmpleado
                    });
                }
                return RequestResult<List<MovimientoCasinoDto>>.CreateSuccess(result);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<MovimientoCasinoDto> GetById(int idMovimientoCasino)
        {
            try
            {
                var movimientoCasino = _Context.MovimientoCasinos.Where(x => x.Id == idMovimientoCasino).FirstOrDefault();
                if (movimientoCasino is null) return RequestResult<MovimientoCasinoDto>.CreateNoSuccess($"No existe el movimiento de casino con identificador {idMovimientoCasino}");
                var resultado = new MovimientoCasinoDto()
                {
                    IdMovimientoCasino = movimientoCasino.Id,
                    Costo = movimientoCasino.Costo,
                    HoraRegistro = movimientoCasino.HoraRegistro,
                    IdEmpleado = movimientoCasino.IdEmpleado,
                    IdTipoComida = movimientoCasino.IdTipoComida,
                    IdGrupoEmpleado = movimientoCasino.IdGrupoEmpleado
                };
                return RequestResult<MovimientoCasinoDto>.CreateSuccess(resultado);


            }
            catch (Exception ex)
            {
                return RequestResult<MovimientoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<MovimientoCasinoDto> Update(MovimientoCasinoDto movimientoCasino)
        {
            try
            {
                if (movimientoCasino == null || movimientoCasino.IdMovimientoCasino == 0)
                {
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Datos incorrectos para la actualización.");
                }

                var entidad = _Context.MovimientoCasinos
                                      .Where(x => x.Id.Equals(movimientoCasino.IdMovimientoCasino))
                                      .FirstOrDefault();

                if (entidad == null)
                {
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("MovimientoCasino no encontrado para la actualización.");
                }

                entidad.Costo = movimientoCasino.Costo;

                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                return RequestResult<MovimientoCasinoDto>.CreateSuccess(new MovimientoCasinoDto
                {
                    Costo = entidad.Costo,
                    HoraRegistro = entidad.HoraRegistro,
                    IdEmpleado = entidad.IdEmpleado,
                    IdTipoComida = entidad.IdTipoComida,
                    IdGrupoEmpleado = entidad.IdGrupoEmpleado,
                    IdMovimientoCasino = entidad.Id 
                });
            }
            catch (Exception ex)
            {
                return RequestResult<MovimientoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

    }
}
