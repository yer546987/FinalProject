using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class GrupoEmpleadoServices : IGrupoEmpleadoServices
    {
        private CasinoAppContext _Context;

        public GrupoEmpleadoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<GrupoEmpleadoDto> Create(GrupoEmpleadoDto grupoEmpleado)
        {
            try
            {
                if (grupoEmpleado is null)
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("Los datos son requeridos");
                GrupoEmpleado entity = new GrupoEmpleado();
                entity.NombreGrupo = grupoEmpleado.NombreGrupo;
                var result = _Context.GrupoEmpleados.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el grupo del empleado.");
                var resultado = new GrupoEmpleadoDto()
                {
                    Id = result.Entity.Id,
                    NombreGrupo = result.Entity.NombreGrupo
                };
                return RequestResult<GrupoEmpleadoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<GrupoEmpleadoDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idGrupoEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<GrupoEmpleadoDto>> GetAll()
        {
            try
            {
                var grupoEmpleado = _Context.GrupoEmpleados.ToList();
                List<GrupoEmpleadoDto> result = new List<GrupoEmpleadoDto>();
                foreach (var item in grupoEmpleado)
                {
                    result.Add(new GrupoEmpleadoDto()
                    {
                        Id = item.Id,
                        NombreGrupo = item.NombreGrupo
                    });
                }
                return RequestResult<List<GrupoEmpleadoDto>>.CreateSuccess(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<GrupoEmpleadoDto> GetById(int idGrupoEmpleado)
        {
            try
            {
                var grupoEmpleado = _Context.GrupoEmpleados.Where(x => x.Id == idGrupoEmpleado).FirstOrDefault();
                if (grupoEmpleado is null) return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess($"No existe el grupo de empleado con identificador {idGrupoEmpleado}");
                var resultado = new GrupoEmpleadoDto()
                {
                    Id = grupoEmpleado.Id,
                    NombreGrupo = grupoEmpleado.NombreGrupo
                };
                return RequestResult<GrupoEmpleadoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<GrupoEmpleadoDto>.CreateError($"Ha ocurrido un error : {ex.Message}");
            }
        }

        public RequestResult<GrupoEmpleadoDto> Update(GrupoEmpleadoDto grupoEmpleado)
        {
            try
            {
                if (grupoEmpleado == null || grupoEmpleado.Id == 0 || string.IsNullOrEmpty(grupoEmpleado.NombreGrupo))
                {
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("Datos de GrupoEmpleado no válidos");
                }

                var entidad = _Context.GrupoEmpleados
                                        .FirstOrDefault(x => x.Id.Equals(grupoEmpleado.Id));

                if (entidad == null)
                {
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("GrupoEmpleado no encontrado");
                }

                entidad.NombreGrupo = grupoEmpleado.NombreGrupo;
                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                var resultado = new GrupoEmpleadoDto()
                {
                    Id = entidad.Id,
                    NombreGrupo = entidad.NombreGrupo
                };

                return RequestResult<GrupoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar GrupoEmpleado: {ex.Message}");
                return RequestResult<GrupoEmpleadoDto>.CreateError($"Error al actualizar GrupoEmpleado: {ex.Message}");
            }
        }

    }
}
