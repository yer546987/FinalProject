using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Services
{
    public class MovimientoCasinoServices : IMovimientoCasinoServices
    {
        private CasinoAppContext _Context;
        public RequestResult<MovimientoCasinoDto> Create(MovimientoCasinoDto movimientoCasino)
        {
            try
            {
                if (movimientoCasino is null)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(movimientoCasino.NombreTipoComida))
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("El nombre del tipo de comida es requerido");
                MovimientoCasino entity = new MovimientoCasino();
                entity.IdTipoComida = movimientoCasino.IdTipoComida;
                var result = _Context.MovimientoCasinos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el movimiento de casino.");
                var resultado = new MovimientoCasinoDto()
                {
                    HoraRegistro = result.Entity.HoraRegistro,
                    Costo = result.Entity.Costo,
                    IdEmpleado = result.Entity.IdEmpleado,
                    IdGrupoEmpleado = result.Entity.IdGrupoEmpleado,
                    IdTipoComida = result.Entity.IdTipoComida

 
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
                    // Pro
                    result.Add(new MovimientoCasinoDto()
                    {
                        HoraRegistro = item.HoraRegistro,
                        Costo = item.Costo,
                        IdEmpleado = item.IdEmpleado,
                        IdGrupoEmpleado = item.IdGrupoEmpleado,
                        IdTipoComida = item.IdTipoComida
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
                if (movimientoCasino == null) return RequestResult<MovimientoCasinoDto>.CreateNoSuccess($"No existe el movimiento de casino con identificador {idMovimientoCasino}");
                var resultado = new MovimientoCasinoDto()
                {
                    HoraRegistro = movimientoCasino.HoraRegistro,
                    Costo = movimientoCasino.Costo,
                    IdEmpleado = movimientoCasino.IdEmpleado,
                    IdGrupoEmpleado = movimientoCasino.IdGrupoEmpleado,
                    IdTipoComida = movimientoCasino.IdTipoComida
                };
                return RequestResult<MovimientoCasinoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<MovimientoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public MovimientoCasinoDto Update(MovimientoCasinoDto movimientoCasino)
        {
            if (movimientoCasino == null) return null;
            if (movimientoCasino.IdMovimientoCasino is 0) return null;
            if (string.IsNullOrEmpty(movimientoCasino.NombreTipoComida)) return null;
            var entidad = _Context.MovimientoCasinos
                                .Where(x => x.Id.Equals(movimientoCasino.IdTipoComida))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Costo = movimientoCasino.Costo;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new MovimientoCasinoDto()
            {
                HoraRegistro = entidad.HoraRegistro,
                Costo = entidad.Costo,
                IdEmpleado = entidad.IdEmpleado,
                IdGrupoEmpleado = entidad.IdGrupoEmpleado,
                IdTipoComida = entidad.IdTipoComida
            };
        }
    }
}
