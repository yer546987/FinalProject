using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Application.Services
{
    public class TipoEmpleadoServices : ITipoEmpleadoServices
    {
        private CasinoAppContext _context;

        public TipoEmpleadoServices() 
        {
            _context = new CasinoAppContext();
        }
        public RequestResult<TipoEmpleadoDto> Create(TipoEmpleadoDto tipoEmpleado)
        {
            try
            {
                if (tipoEmpleado is null)
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(tipoEmpleado.Nombre))
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("El nombre del empleado es requerido");
                TipoEmpleado entity = new TipoEmpleado();
                entity.Nombre = tipoEmpleado.Nombre;
                var result = _context.TipoEmpleados.Add(entity);
                int rows = _context.SaveChanges();
                if (rows is 0)
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el empleado.");
                var resultado = new TipoEmpleadoDto()
                {
                    Id = result.Entity.Id,
                    Nombre = result.Entity.Nombre
   
                };
                return RequestResult<TipoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoEmpleadoDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idTipoEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoEmpleadoDto>> GetAll()
        {
            try
            {
                var tipoEmpleado = _context.TipoEmpleados.ToList();
                List<TipoEmpleadoDto> result = new List<TipoEmpleadoDto>();
                foreach (var item in tipoEmpleado)
                {
                    result.Add(new TipoEmpleadoDto()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                    });
                }
                return RequestResult<List<TipoEmpleadoDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<TipoEmpleadoDto> GetById(int idTipoEmpleado)
        {
            try
            {
                var tipoEmpleado = _context.TipoEmpleados.Where(x => x.Id == idTipoEmpleado).FirstOrDefault();
                if (tipoEmpleado == null) return RequestResult<TipoEmpleadoDto>.CreateNoSuccess($"No existe el tipo de empleado con identificador {idTipoEmpleado}");
                var resultado = new TipoEmpleadoDto()
                {
                    Id = tipoEmpleado.Id,
                    Nombre = tipoEmpleado.Nombre
                };
                return RequestResult<TipoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoEmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public TipoEmpleadoDto Update(TipoEmpleadoDto tipoEmpleado)
        {
            if (tipoEmpleado == null) return null;
            if (tipoEmpleado.Id is 0) return null;
            if (string.IsNullOrEmpty(tipoEmpleado.Nombre)) return null;
            var entidad = _context.TipoEmpleados
                                .Where(x => x.Id.Equals(tipoEmpleado.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Nombre = tipoEmpleado.Nombre;
            _context.Attach(entidad);
            _context.Entry(entidad).State = EntityState.Modified;
            _context.SaveChanges();
            return new TipoEmpleadoDto()
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre
            };
        }
    }
}
