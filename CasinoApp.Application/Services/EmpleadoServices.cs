using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Services
{
    public class EmpleadoServices : IEmpleadoSercvices
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
                if (string.IsNullOrEmpty(empleado.NombreEmpleado))
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("El nombre del empleado es requerido");
                Empleado entity = new Empleado();
                entity.Nombre = empleado.NombreEmpleado;
                var result = _Context.Empleados.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<EmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al regitrar el empleado.");
                var resultado = new EmpleadoDto()
                {
                    IdEmpleado = result.Entity.Id,
                    NombreEmpleado = result.Entity.Nombre,
                    ApellidoE = result.Entity.Apellido,
                    IdTipoIdentificacionE = result.Entity.IdTipoIdentificacion,
                    IdentificacionE = result.Entity.Identificacion,
                    IdGrupoEE = result.Entity.IdGrupoE,
                    InternoE = result.Entity.Interno,
                    IdTipoEmpleadoE = result.Entity.IdTipoEmpleado
                };
                return RequestResult<EmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<EmpleadoDto>.CreateError(ex.Message);
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
                        IdEmpleado = item.Id,
                        NombreEmpleado = item.Nombre,
                        ApellidoE = item.Apellido,
                        IdTipoIdentificacionE = item.IdTipoIdentificacion,
                        IdentificacionE = item.Identificacion,
                        IdGrupoEE = item.IdGrupoE,
                        InternoE = item.Interno,
                        IdTipoEmpleadoE = item.IdTipoEmpleado
                    });
                }
                return RequestResult<List<EmpleadoDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<EmpleadoDto> GetById(int idEmpleado)
        {
            try
            {
                var empleado = _Context.Empleados.Where(x => x.Id == idEmpleado).FirstOrDefault();
                if (empleado == null) return RequestResult<EmpleadoDto>.CreateNoSuccess($"No existe el empleado con identificación {idEmpleado}");
                var resultados = new EmpleadoDto()
                {
                    IdEmpleado = empleado.Id,
                    NombreEmpleado = empleado.Nombre,
                    ApellidoE = empleado.Apellido,
                    IdTipoIdentificacionE = empleado.IdTipoIdentificacion,
                    IdentificacionE = empleado.Identificacion,
                    IdGrupoEE = empleado.IdGrupoE,
                    InternoE = empleado.Interno,
                    IdTipoEmpleadoE = empleado.IdTipoEmpleado
                };
                 return RequestResult<EmpleadoDto>.CreateSuccess(resultados);
            }
            catch (Exception ex)
            {
                return RequestResult<EmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public EmpleadoDto Update(EmpleadoDto empleado)
        {
            if (empleado is null)return null;
            if (empleado.IdEmpleado is 0) return null;
            if (string.IsNullOrEmpty(empleado.NombreEmpleado)) return null;
            var Empleado = _Context.Empleados
                .Where(x => x.Id.Equals(empleado.IdEmpleado))
                .FirstOrDefault();
            if (Empleado is null) return null;
            Empleado.Nombre = empleado.NombreEmpleado;
            _Context.Attach(Empleado);
            _Context.Entry(Empleado).State = EntityState.Modified;
            _Context.SaveChanges();
            return new EmpleadoDto()
            {
                    IdEmpleado = Empleado.Id,
                    NombreEmpleado = Empleado.Nombre,
                    ApellidoE = Empleado.Apellido,
                    IdTipoIdentificacionE = Empleado.IdTipoIdentificacion,
                    IdentificacionE = Empleado.Identificacion,
                    IdGrupoEE = Empleado.IdGrupoE,
                    InternoE = Empleado.Interno,
                    IdTipoEmpleadoE = Empleado.IdTipoEmpleado   
            };
        }
    }
}
