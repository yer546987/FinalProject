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
                if (string.IsNullOrEmpty(inventario.Producto))
                    return RequestResult<InventarioDto>.CreateNoSuccess("El producto es requerido");
                Inventario entity = new Inventario();
                entity.Producto = inventario.Producto;
                var result = _Context.Inventarios.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<InventarioDto>.CreateNoSuccess("Ha ocurrido un error al registrar el producto.");
                var resultado = new InventarioDto()
                {
                    Producto = result.Entity.Producto,
                    Cantidad = result.Entity.Cantidad,
                    FechaVencimiento = result.Entity.FechaVencimiento,
                    Id = result.Entity.Id,
                    IdInventario = result.Entity.IdInventario,
                    IdUnidadMedida = result.Entity.IdUnidadMedida,
                    Mecatos = result.Entity.Mecatos,
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
                        Producto = item.Producto,
                        Cantidad = item.Cantidad,
                        FechaVencimiento = item.FechaVencimiento,
                        Id = item.Id,
                        IdInventario = item.IdInventario,
                        IdUnidadMedida = item.IdUnidadMedida,
                        Mecatos = item.Mecatos,
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
                    Producto = inventario.Producto,
                    Cantidad = inventario.Cantidad,
                    FechaVencimiento = inventario.FechaVencimiento,
                    Id = inventario.Id,
                    IdInventario = inventario.IdInventario,
                    IdUnidadMedida = inventario.IdUnidadMedida,
                    Mecatos = inventario.Mecatos,
                    Stock = inventario.Stock
                };
                return RequestResult<InventarioDto>.CreateSuccess(resultado);


            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public InventarioDto Update(InventarioDto inventario)
        {
            if (inventario is null) return null;
            if (inventario.Id is 0) return null;
            if (string.IsNullOrEmpty(inventario.Producto)) return null;
            var entidad = _Context.Inventarios
                                .Where(x => x.Id.Equals(inventario.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Producto = inventario.Producto;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new InventarioDto()
            {
                Producto = entidad.Producto,
                Cantidad = entidad.Cantidad,
                FechaVencimiento = entidad.FechaVencimiento,
                Id = entidad.Id,
                IdInventario = entidad.IdInventario,
                IdUnidadMedida = entidad.IdUnidadMedida,
                Mecatos = entidad.Mecatos,
                Stock = entidad.Stock
            };
        }
    }
}
