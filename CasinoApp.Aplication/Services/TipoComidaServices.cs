using Azure;
using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoDocumento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

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
                TipoComida entity = new TipoComida();
                entity.Nombre = tipoComida.Nombre;
                entity.Descripcion = tipoComida.Descripcion;
                entity.Cronograma = tipoComida.Cronograma;
                entity.Limite = tipoComida.Limite;
                entity.Precio = tipoComida.Precio;
                entity.TiempoInicial = tipoComida.TiempoInicial;
                entity.TiempoFinal = tipoComida.TiempoFinal;
                
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
                    TiempoInicial = result.Entity.TiempoInicial,
                   

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
                        TiempoInicial = item.TiempoInicial,
                        
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
                    TiempoInicial = tipoComida.TiempoInicial,
                    
                };
                return RequestResult<TipoComidaDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<TipoComidaDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<TipoComidaDto> Update(TipoComidaDto tipoComida)
        {
            try
            {
                if (tipoComida == null || tipoComida.Id == 0 || string.IsNullOrEmpty(tipoComida.Nombre))
                {
                    return RequestResult<TipoComidaDto>.CreateNoSuccess("Datos de TipoComida no válidos");
                }

                var entidad = _Context.TipoComida
                                        .FirstOrDefault(x => x.Id.Equals(tipoComida.Id));

                if (entidad == null)
                {
                    return RequestResult<TipoComidaDto>.CreateNoSuccess("TipoComida no encontrada");
                }

                entidad.Nombre = tipoComida.Nombre;
                entidad.Descripcion = tipoComida.Descripcion;
                entidad.Cronograma = tipoComida.Cronograma;
                entidad.Limite = tipoComida.Limite;
                entidad.Precio = tipoComida.Precio;
                entidad.TiempoInicial = tipoComida.TiempoInicial;
                entidad.TiempoFinal = tipoComida.TiempoFinal;
               

                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                var resultado = new TipoComidaDto()
                {
                    Id = entidad.Id,
                    Nombre = entidad.Nombre,
                    Descripcion = entidad.Descripcion,
                    Cronograma = entidad.Cronograma,
                    Limite = entidad.Limite,
                    Precio = entidad.Precio,
                    TiempoInicial = entidad.TiempoInicial,
                    TiempoFinal = entidad.TiempoFinal,
                
                };

                return RequestResult<TipoComidaDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar TipoComida: {ex.Message}");
                return RequestResult<TipoComidaDto>.CreateError($"Error al actualizar TipoComida: {ex.Message}");
            }
        }
    }
}
