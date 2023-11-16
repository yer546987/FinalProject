using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class TipoEmpleadoServices : ITipoEmpleadoServices
    {
        private CasinoAppContext _Context;

        public TipoEmpleadoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<TipoEmpleadoDto> Create(TipoEmpleadoDto tipoEmpleado)
        {
            try
            {
                if (tipoEmpleado is null)
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("Los datos son requeridos");
                TipoEmpleado entity = new TipoEmpleado();
                entity.Nombre = tipoEmpleado.Nombre;
                var result = _Context.TipoEmpleados.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el tipo de empleado.");
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
                var tipoEmpleado = _Context.TipoEmpleados.ToList();
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
                var tipoEmpleado = _Context.TipoEmpleados.Where(x => x.Id == idTipoEmpleado).FirstOrDefault();
                if (tipoEmpleado is null) return RequestResult<TipoEmpleadoDto>.CreateNoSuccess($"No existe el tipo de empleado con identificador {idTipoEmpleado}");
                var resultado = new TipoEmpleadoDto()
                {
                    Id = tipoEmpleado.Id,
                    Nombre = tipoEmpleado.Nombre,
                };
                return RequestResult<TipoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoEmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<TipoEmpleadoDto> Update(TipoEmpleadoDto tipoEmpleado)
        {
            try
            {
                if (tipoEmpleado == null || tipoEmpleado.Id == 0 || string.IsNullOrEmpty(tipoEmpleado.Nombre))
                {
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("Datos de TipoEmpleado no válidos");
                }

                var entidad = _Context.TipoEmpleados
                                        .FirstOrDefault(x => x.Id.Equals(tipoEmpleado.Id));

                if (entidad == null)
                {
                    return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("TipoEmpleado no encontrado");
                }

                entidad.Nombre = tipoEmpleado.Nombre;
                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                var resultado = new TipoEmpleadoDto()
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                };

                return RequestResult<TipoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar TipoEmpleado: {ex.Message}");
                return RequestResult<TipoEmpleadoDto>.CreateError($"Error al actualizar TipoEmpleado: {ex.Message}");
            }
        }

    }
}
