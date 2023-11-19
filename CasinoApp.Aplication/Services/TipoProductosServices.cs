using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoEmpleado;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Aplication.Services
{
    public class TipoProductosServices : ITipoProductosSeervices
    {
        private CasinoAppContext _Context;

        public TipoProductosServices()
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<TipoProductoDto> Create(TipoProductoDto tipoProducto)
        {
            try
            {
                if (tipoProducto is null)
                    return RequestResult<TipoProductoDto>.CreateNoSuccess("Los datos son requeridos");
                TipoProducto entity = new TipoProducto();
                entity.Tipo = tipoProducto.TipoProducto;
                var result = _Context.TipoProductos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<TipoProductoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el empleado.");
                var resultado = new TipoProductoDto()
                {
                    IdTipoProducto = result.Entity.Id,
                    TipoProducto = result.Entity.Tipo,
                };
                return RequestResult<TipoProductoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<TipoProductoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public bool Delete(int Idtipo)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoProductoDto>> GetAll()
        {
            try
            {
                var tipoProducto = _Context.TipoProductos.ToList();
                List<TipoProductoDto> result = new List<TipoProductoDto>();
                foreach (var item in tipoProducto)
                {
                    result.Add(new TipoProductoDto()
                    {
                        IdTipoProducto = item.Id,
                        TipoProducto = item.Tipo
                    });
                }
                return RequestResult<List<TipoProductoDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<TipoProductoDto> GetById(int Id)
        {
            try
            {
                var tipoProducto = _Context.TipoProductos.Where(x => x.Id == Id).FirstOrDefault();
                if (tipoProducto is null) return RequestResult<TipoProductoDto>.CreateNoSuccess($"No existe el tipo de producto con identificador {Id}");
                var resultado = new TipoProductoDto()
                {
                    IdTipoProducto = tipoProducto.Id,
                    TipoProducto = tipoProducto.Tipo
                };
                return RequestResult<TipoProductoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<TipoProductoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<TipoProductoDto> Update(TipoProductoDto tipoProducto)
        {
            try
            {
                if (tipoProducto == null || string.IsNullOrEmpty(tipoProducto.TipoProducto))
                {
                    return RequestResult<TipoProductoDto>.CreateNoSuccess("Datos de Tipo de producto no válidos");
                }

                var entidad = _Context.TipoProductos
                                        .FirstOrDefault(x => x.Id.Equals(tipoProducto.IdTipoProducto));

                if (entidad == null)
                {
                    return RequestResult<TipoProductoDto>.CreateNoSuccess("Tipo de producto no encontrado");
                }

                entidad.Tipo = tipoProducto.TipoProducto;
                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                var resultado = new TipoProductoDto()
                {
                    IdTipoProducto = entidad.Id,
                    TipoProducto = entidad.Tipo
                };

                return RequestResult<TipoProductoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el tipo de producto: {ex.Message}");
                return RequestResult<TipoProductoDto>.CreateError($"Error al actualizar el tipo de producto: {ex.Message}");
            }
        }
    }
}
