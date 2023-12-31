﻿using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Services
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
                if (string.IsNullOrEmpty(grupoEmpleado.NombreGrupo))
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("El Nombre del grupo de empleado es requerido");
                GrupoEmpleado entity = new GrupoEmpleado();
                entity.NombreGrupo = grupoEmpleado.NombreGrupo;
                var result = _Context.GrupoEmpleados.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("Ha ocurrido un error al reguistrar el grupo de empleado.");
                var resultado = new GrupoEmpleadoDto()
                {
                    Id = result.Entity.Id,
                    NombreGrupo = result.Entity.NombreGrupo
 
                };
                return RequestResult<GrupoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception)
            {

                throw;
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
            catch (Exception ex)
            {
                throw;
 
            }
        }

        public RequestResult<GrupoEmpleadoDto> GetById(int idGrupoEmpleado)
        {
            try
            {
                var grupoEmpleado = _Context.GrupoEmpleados.Where(x => x.Id == idGrupoEmpleado).FirstOrDefault();
                if (grupoEmpleado == null) return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess($"No existe el grupo de empleado con identificador {idGrupoEmpleado}");
                var resultado = new GrupoEmpleadoDto()
                {
                    Id = grupoEmpleado.Id,
                    NombreGrupo = grupoEmpleado.NombreGrupo
                };
                return RequestResult<GrupoEmpleadoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<GrupoEmpleadoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public GrupoEmpleadoDto Update(GrupoEmpleadoDto grupoEmpleado)
        {
            if (grupoEmpleado == null) return null;
            if (grupoEmpleado.Id is 0) return null;
            if (string.IsNullOrEmpty(grupoEmpleado.NombreGrupo)) return null;
            var entidad = _Context.GrupoEmpleados
                                .Where(x => x.Id.Equals(grupoEmpleado.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.NombreGrupo = grupoEmpleado.NombreGrupo;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new GrupoEmpleadoDto() 
            { 
                    Id = entidad.Id,
                    NombreGrupo = entidad.NombreGrupo
            };
        }
    }
}
