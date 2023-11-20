using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class InventarioServices : IInventarioServices
    {
        private CasinoAppContext _Context;

        public InventarioServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<InventarioDto> Create(InventarioDto inventario)
        {
            try
            {
                if (inventario is null)
                    return RequestResult<InventarioDto>.CreateNoSuccess("Los datos son requeridos");
                Inventario entity = new Inventario();
                entity.Cantidad = inventario.Cantidad;
                entity.Stock = inventario.Stock;
                entity.IdProducto = inventario.IdProducto;
                var result = _Context.Inventarios.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<InventarioDto>.CreateNoSuccess("Ha ocurrido un error al registrar el producto.");
                var resultado = new InventarioDto()
                {
                    IdProducto = result.Entity.IdProducto,
                    Cantidad = result.Entity.Cantidad,                  
                    Id = result.Entity.Id,                  
                    Stock = result.Entity.Stock
                };
                return RequestResult<InventarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idinventario)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<InventarioDto>> GetAll()
        {
            try
            {
                var inventario = _Context.Inventarios.ToList();
                List<InventarioDto> result = new List<InventarioDto>();
                foreach (var item in inventario)
                {
                    result.Add(new InventarioDto()
                    {
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,                      
                        Id = item.Id,                        
                        Stock = item.Stock
                    });
                }
                return RequestResult<List<InventarioDto>>.CreateSuccess(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<InventarioDto> GetById(int idinventario)
        {
            try
            {
                var inventario = _Context.Inventarios.Where(x => x.Id == idinventario).FirstOrDefault();
                if (inventario is null) return RequestResult<InventarioDto>.CreateNoSuccess($"No existe el inventario con identificador {idinventario}");
                var resultado = new InventarioDto()
                {
                    IdProducto = inventario.IdProducto,
                    Cantidad = inventario.Cantidad,                
                    Id = inventario.Id,                   
                    Stock = inventario.Stock
                };
                return RequestResult<InventarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<InventarioDto> Update(InventarioDto inventario)
        {
            try
            {
                if (inventario == null || inventario.Id == 0)
                {
                    return RequestResult<InventarioDto>.CreateNoSuccess("Datos incorrectos para la actualización.");
                }

                var entidad = _Context.Inventarios
                                      .Where(x => x.Id.Equals(inventario.Id))
                                      .FirstOrDefault();

                if (entidad == null)
                {
                    return RequestResult<InventarioDto>.CreateNoSuccess("Inventario no encontrado para la actualización.");
                }

                entidad.IdProducto = inventario.IdProducto;

                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                return RequestResult<InventarioDto>.CreateSuccess(new InventarioDto
                {
                    IdProducto = entidad.IdProducto,
                    Cantidad = entidad.Cantidad,
                    Id = entidad.Id,
                    Stock = entidad.Stock
                });
            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

    }
}
