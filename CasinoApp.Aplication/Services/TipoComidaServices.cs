using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoDocumento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class TipoComidaServices : ITipoComidaServices
    {
        private CasinoAppContext _Context;

        public TipoComidaServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<TipoComidaDto> Create(TipoComidaDto tipoComida)
        {
            try
            {
                if (tipoComida is null)
                    return RequestResult<TipoComidaDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(tipoComida.Nombre))
                    return RequestResult<TipoComidaDto>.CreateNoSuccess("El nombre del tipo de comida es requerido");
                TipoComidum entity = new TipoComidum();
                entity.Nombre = tipoComida.Nombre;
                var result = _Context.TipoComida.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<TipoComidaDto>.CreateNoSuccess("Ha ocurrido un error al registrar el tipo de comida.");
                var resultado = new TipoComidaDto()
                {
                    Id = result.Entity.Id,
                    Nombre = result.Entity.Nombre,
                    Descripcion = result.Entity.Descripcion,
                    Cronograma = result.Entity.Cronograma,
                    Limite = result.Entity.Limite,
                    Precio = result.Entity.Precio,
                    TiempoFinal = result.Entity.TiempoFinal,
                    TiempoInicial = result.Entity.TiempoInicial

                };
                return RequestResult<TipoComidaDto>.CreateSuccess(resultado);
            
            }
            catch (Exception ex)
            {
                return RequestResult<TipoComidaDto>.CreateError(ex.Message);    
            }
        }

        public bool Delete(int idTípoComida)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoComidaDto>> GetAll()
        {
            try
            {
                var tipoComida = _Context.TipoComida.ToList();
                List<TipoComidaDto> result = new List<TipoComidaDto>();
                foreach (var item in tipoComida)
                {
                    result.Add(new TipoComidaDto()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                        Cronograma = item.Cronograma,
                        Limite = item.Limite,
                        Precio = item.Precio,
                        TiempoFinal = item.TiempoFinal,
                        TiempoInicial = item.TiempoInicial
                    });
                }
                return RequestResult<List<TipoComidaDto>>.CreateSuccess(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<TipoComidaDto> GetById(int idTipoComida)
        {
            try
            {
                var tipoComida = _Context.TipoComida.Where(x => x.Id == idTipoComida).FirstOrDefault();
                if (tipoComida is null) return RequestResult<TipoComidaDto>.CreateNoSuccess($"No existe el tipo de comida con identificador {idTipoComida}");
                var resultado = new TipoComidaDto()
                {
                    Id = tipoComida.Id,
                    Nombre = tipoComida.Nombre,
                    Descripcion = tipoComida.Descripcion,
                    Cronograma = tipoComida.Cronograma,
                    Limite = tipoComida.Limite,
                    Precio = tipoComida.Precio,
                    TiempoFinal = tipoComida.TiempoFinal,
                    TiempoInicial = tipoComida.TiempoInicial
                };
                return RequestResult<TipoComidaDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<TipoComidaDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public TipoComidaDto Update(TipoComidaDto tipoComida)
        {
            if (tipoComida is null) return null;
            if (tipoComida.Id is 0) return null;
            if (string.IsNullOrEmpty(tipoComida.Nombre)) return null;
            var entidad = _Context.TipoComida
                                .Where(x => x.Id.Equals(tipoComida.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Nombre = tipoComida.Nombre;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new TipoComidaDto()
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Descripcion = entidad.Descripcion,
                Cronograma = entidad.Cronograma,
                Limite = entidad.Limite,
                Precio = entidad.Precio,
                TiempoFinal = entidad.TiempoFinal,
                TiempoInicial = entidad.TiempoInicial
            };
        }
    }
}
