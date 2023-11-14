using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.UnidadMedida;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Application.Services
{
    public class UnidadMedidaServices : IUnidadMedidaServices
    {
        private CasinoAppContext _Context;

        public UnidadMedidaServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<UnidadMedidaDto> Create(UnidadMedidaDto unidadMedida)
        {
            try
            {
                if (unidadMedida is null)
                    return RequestResult<UnidadMedidaDto>.CreateNoSuccess("Los datos son requeridos.");
                if (string.IsNullOrEmpty(unidadMedida.Nombre))
                    return RequestResult<UnidadMedidaDto>.CreateNoSuccess("El nombre de la unidad de medida es requerido.");
                UnidadMedidum entity = new UnidadMedidum();
                entity.Nombre = unidadMedida.Nombre;
                var result = _Context.UnidadMedida.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<UnidadMedidaDto>.CreateNoSuccess("Ha ocurrido un error al registrar la unidad de medida.");
                var resultado = new UnidadMedidaDto()
                {
                    Id = result.Entity.Id,
                    Nombre = result.Entity.Nombre
                };
                return RequestResult<UnidadMedidaDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<UnidadMedidaDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idUnidadMedida)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<UnidadMedidaDto>> GetAll()
        {
            try
            {
                var unidadMedida = _Context.UnidadMedida.ToList();
                List<UnidadMedidaDto> result = new List<UnidadMedidaDto>();
                foreach (var item in unidadMedida)
                {
                    // Pro
                    result.Add(new UnidadMedidaDto()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre
                    });
                }
                return RequestResult<List<UnidadMedidaDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<UnidadMedidaDto> GetById(Guid idUnidadMedida)
        {
            try
            {
                var unidadMedida = _Context.UnidadMedida.Where(x => x.Id == idUnidadMedida).FirstOrDefault();
                if (unidadMedida == null) return RequestResult<UnidadMedidaDto>.CreateNoSuccess($"No existe la unidad de medida con identificador {idUnidadMedida}");
                var resultado = new UnidadMedidaDto()
                {
                    Id = unidadMedida.Id,
                    Nombre = unidadMedida.Nombre
                };
                return RequestResult<UnidadMedidaDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<UnidadMedidaDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public UnidadMedidaDto Update(UnidadMedidaDto unidadMedida)
        {
            if (unidadMedida == null) return null;
            if (unidadMedida.Id.Equals(Guid.Empty)) return null;
            if (string.IsNullOrEmpty(unidadMedida.Nombre)) return null;
            var entidad = _Context.UnidadMedida
                                .Where(x => x.Id.Equals(unidadMedida.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Nombre = unidadMedida.Nombre;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new UnidadMedidaDto()
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre
            };
        }
    }
}
