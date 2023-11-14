using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Application.Services
{
    public class TipoDocumentoServices : ITipoDocumentoServices
    {
        private CasinoAppContext _Context;

        public TipoDocumentoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<TipoDocumentoDto> Create(TipoDocumentoDto tipoDocumento)
        {
            try
            {
                if (tipoDocumento is null)
                    return RequestResult<TipoDocumentoDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(tipoDocumento.TipoIdentificacion))
                    return RequestResult<TipoDocumentoDto>.CreateNoSuccess("El tipo de documento es requerido");
                TipoDocumento entity = new TipoDocumento();
                entity.TipoIdentificacion = tipoDocumento.TipoIdentificacion;
                var result = _Context.TipoDocumentos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<TipoDocumentoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el tipo de decumento.");
                var resultado = new TipoDocumentoDto()
                {
                    Id = result.Entity.Id,
                    TipoIdentificacion = result.Entity.TipoIdentificacion
                };
                return RequestResult<TipoDocumentoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoDocumentoDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idTipoDocumento)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoDocumentoDto>> GetAll()
        {
            try
            {
                var tipoDocumento = _Context.TipoDocumentos.ToList();
                List<TipoDocumentoDto> result = new List<TipoDocumentoDto>();
                foreach (var item in tipoDocumento)
                {
                    result.Add(new TipoDocumentoDto()
                    {
                        Id = item.Id,
                        TipoIdentificacion = item.TipoIdentificacion
                    });
                }
                return RequestResult<List<TipoDocumentoDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<TipoDocumentoDto> GetById(Guid idTipoDocumento)
        {
            try
            {
                var tipoDocumento = _Context.TipoDocumentos.Where(x => x.Id == idTipoDocumento).FirstOrDefault();
                if (tipoDocumento == null) return RequestResult<TipoDocumentoDto>.CreateNoSuccess($"No existe el tipo de documento con identificador {idTipoDocumento}");
                var resultado = new TipoDocumentoDto()
                {
                    Id = tipoDocumento.Id,
                    TipoIdentificacion = tipoDocumento.TipoIdentificacion
                };
                return RequestResult<TipoDocumentoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoDocumentoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public TipoDocumentoDto Update(TipoDocumentoDto tipoDocumento)
        {
            if (tipoDocumento == null) return null;
            if (tipoDocumento.Id.Equals(Guid.Empty)) return null;
            if (string.IsNullOrEmpty(tipoDocumento.TipoIdentificacion)) return null;
            var entidad = _Context.TipoDocumentos
                                .Where(x => x.Id.Equals(tipoDocumento.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.TipoIdentificacion = tipoDocumento.TipoIdentificacion;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new TipoDocumentoDto()
            {
                Id = entidad.Id,
                TipoIdentificacion = entidad.TipoIdentificacion
            };
        }
    }
}
