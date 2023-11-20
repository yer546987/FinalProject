using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class ProductoServices : IProductosServices
    {
        private CasinoAppContext _Context;
        public ProductoServices()
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<ProductoDto> Create(ProductoDto producto)
        {
            try
            {
                if (producto is null)
                    return RequestResult<ProductoDto>.CreateNoSuccess("Los datos son requeridos");
                Producto entity = new Producto();
                entity.Nombre = producto.NombreProducto;
                entity.FechaVencimiento = producto.FechaVencimientoProducto;
                entity.IdTipoProducto = producto.IdTipoProducto;
                var result = _Context.Productos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<ProductoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el empleado.");
                var resultado = new ProductoDto()
                {
                    IdProducto = result.Entity.Id,
                    IdTipoProducto = result.Entity.IdTipoProducto,
                    FechaVencimientoProducto = result.Entity.FechaVencimiento,
                    NombreProducto = result.Entity.Nombre
                };
                return RequestResult<ProductoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<ProductoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public bool Delete(int idProducto)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<ProductoDto>> GetAll()
        {
            try
            {
                var producto = _Context.Productos.ToList();
                List<ProductoDto> result = new List<ProductoDto>();
                foreach (var item in producto)
                {
                    result.Add(new ProductoDto()
                    {
                        IdProducto = item.Id,
                        NombreProducto = item.Nombre,
                        FechaVencimientoProducto = item.FechaVencimiento,
                        IdTipoProducto = item.IdTipoProducto
                    });
                }
                return RequestResult<List<ProductoDto>>.CreateSuccess(result);


            }
            catch (Exception ex)
            {

                return RequestResult<List<ProductoDto>>.CreateError($"Ha ocurrido un error {ex.Message}");
            }
        }

        public RequestResult<ProductoDto> GetById(int idProducto)
        {
            try
            {
                var producto = _Context.Productos.Where(x => x.Id == idProducto).FirstOrDefault();
                if (producto is null) return RequestResult<ProductoDto>.CreateNoSuccess($"No existe el producto con identificador {idProducto}");
                var resultado = new ProductoDto()
                {
                    IdProducto = producto.Id,
                    NombreProducto = producto.Nombre,
                    FechaVencimientoProducto = producto.FechaVencimiento,
                    IdTipoProducto = producto.IdTipoProducto,
                };
                return RequestResult<ProductoDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<ProductoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<ProductoDto> Update(ProductoDto producto)
        {
            try
            {
                if (producto is null || producto.IdProducto == 0)
                {
                    return RequestResult<ProductoDto>.CreateNoSuccess("Datos incorrectos para la actualización.");
                }

                // Obtener la entidad actual desde la base de datos
                var entidad = _Context.Productos
                                      .Where(x => x.Id.Equals(producto.IdProducto))
                                      .FirstOrDefault();

                if (entidad == null)
                {
                    return RequestResult<ProductoDto>.CreateNoSuccess("Producto no encontrado para la actualización.");
                }

                // Actualizar los valores de la entidad con los nuevos datos
                entidad.Nombre = producto.NombreProducto;
                entidad.FechaVencimiento = producto.FechaVencimientoProducto;
                entidad.IdTipoProducto = producto.IdTipoProducto;

                // Marcar la entidad como modificada y guardar los cambios
                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                // Devolver los datos actualizados del empleado
                return RequestResult<ProductoDto>.CreateSuccess(new ProductoDto
                {
                    IdProducto = entidad.Id,
                    NombreProducto = entidad.Nombre,
                    FechaVencimientoProducto = entidad.FechaVencimiento,
                    IdTipoProducto = entidad.IdTipoProducto
                });
            }
            catch (Exception ex)
            {
                return RequestResult<ProductoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }
    }
}