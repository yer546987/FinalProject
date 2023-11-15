using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class EmpleadoServices : IEmpleadosServices
    {
        private CasinoAppContext _Context;

        public EmpleadoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<EmpleadoDto> Create(EmpleadoDto empleado)
        {
            try
            {
                if (empleado is null)
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("Los datos son requeridos");             
                Empleado entity = new Empleado();
                entity.Nombre = empleado.NombreEmpleado;
                entity.Apellido = empleado.ApellidoE;
                entity.IdGrupoE = empleado.IdGrupoEE;
                entity.IdTipoEmpleado = empleado.IdTipoEmpleadoE;
                entity.Interno = empleado.InternoE;
                entity.IdTipoIdentificacion = empleado.IdTipoIdentificacionE;
                entity.Identificacion = empleado.IdentificacionE;
                var result = _Context.Empleados.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el empleado.");
                var resultado = new EmpleadoDto()
                {
                    NombreEmpleado = result.Entity.Nombre,
                    ApellidoE = result.Entity.Apellido,
                    IdEmpleado = result.Entity.Id, 
                    IdentificacionE = (int)result.Entity.Identificacion,
                    IdGrupoEE = result.Entity.IdGrupoE,
                    IdTipoEmpleadoE = result.Entity.IdTipoEmpleado,
                    IdTipoIdentificacionE = result.Entity.IdTipoIdentificacion,
                    InternoE = result.Entity.Interno

                };
                return RequestResult<EmpleadoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<EmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public bool Delete(int idEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<EmpleadoDto>> GetAll()
        {
            try
            {
                var empleado = _Context.Empleados.ToList();
                List<EmpleadoDto> result = new List<EmpleadoDto>();
                foreach (var item in empleado)
                {
                    result.Add(new EmpleadoDto()
                    {
                        NombreEmpleado = item.Nombre,
                        ApellidoE = item.Apellido,
                        IdEmpleado = item.Id,
                        IdentificacionE = (int)item.Identificacion,
                        IdGrupoEE = item.IdGrupoE,                     
                        IdTipoEmpleadoE = item.IdTipoEmpleado,                      
                        IdTipoIdentificacionE = item.IdTipoIdentificacion,                       
                        InternoE = item.Interno,
                      
                    });
                }
                return RequestResult<List<EmpleadoDto>>.CreateSuccess(result);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<EmpleadoDto> GetById(int Id)
        {
            try
            {
                var empleado = _Context.Empleados.Where(x => x.Id == Id).FirstOrDefault();
                if (empleado is null) return RequestResult<EmpleadoDto>.CreateNoSuccess($"No existe el empleado con identificador {Id}");
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

        public RequestResult<EmpleadoDto> Update(EmpleadoDto empleado)
        {
            try
            {
                if (empleado is null || empleado.IdEmpleado == 0)
                {
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("Datos incorrectos para la actualización.");
                }

                // Obtener la entidad actual desde la base de datos
                var entidad = _Context.Empleados
                                      .Where(x => x.Id.Equals(empleado.IdEmpleado))
                                      .FirstOrDefault();

                if (entidad == null)
                {
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("Empleado no encontrado para la actualización.");
                }

                // Actualizar los valores de la entidad con los nuevos datos
                entidad.Nombre = empleado.NombreEmpleado;
                entidad.Apellido = empleado.ApellidoE;
                entidad.IdGrupoE = empleado.IdGrupoEE;
                entidad.IdTipoEmpleado = empleado.IdTipoEmpleadoE;
                entidad.IdTipoIdentificacion = empleado.IdTipoIdentificacionE;
                entidad.Identificacion = empleado.IdentificacionE;
                entidad.Interno = empleado.InternoE;

                // Marcar la entidad como modificada y guardar los cambios
                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                // Devolver los datos actualizados del empleado
                return RequestResult<EmpleadoDto>.CreateSuccess(new EmpleadoDto
                {
                    NombreEmpleado = entidad.Nombre,
                    ApellidoE = entidad.Apellido,
                    IdEmpleado = entidad.Id,
                    IdentificacionE = (int)entidad.Identificacion,
                    IdGrupoEE = entidad.IdGrupoE,
                    IdTipoEmpleadoE = entidad.IdTipoEmpleado,
                    IdTipoIdentificacionE = entidad.IdTipoIdentificacion,
                    InternoE = entidad.Interno
                });
            }
            catch (Exception ex)
            {
                return RequestResult<EmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }
    }
}
