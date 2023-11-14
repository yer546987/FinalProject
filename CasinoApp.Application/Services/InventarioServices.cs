using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Application.Services
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
                    return RequestResult<InventarioDto>.CreateNoSuccess("El producto del inventario es requerido");
                Inventario entity = new Inventario();
                entity.Producto = inventario.Producto;
                var result = _Context.Inventarios.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<InventarioDto>.CreateNoSuccess("Ha ocurrido un error al registrar el inventario.");
                var resultado = new InventarioDto()
                {
                   Producto = result.Entity.Producto,
                   IdInventario = result.Entity.IdInventario,
                   Cantidad = result.Entity.Cantidad,
                   FechaVencimiento = result.Entity.FechaVencimiento,
                   Mecatos = result.Entity.Mecatos,
                   Id = result.Entity.Id,  
                   IdUnidadMedida = result.Entity.IdUnidadMedida,
                   Stock = result.Entity.Stock
                   
                };
                return RequestResult<InventarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idInventario)
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
                        IdInventario = item.IdInventario,
                        Cantidad = item.Cantidad,
                        FechaVencimiento = item.FechaVencimiento,
                        Mecatos = item.Mecatos,
                        Id = item.Id,
                        IdUnidadMedida = item.IdUnidadMedida,
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

        public RequestResult<InventarioDto> GetById(Guid idInventario)
        {
            try
            {
                var inventario = _Context.Inventarios.Where(x => x.Id == idInventario).FirstOrDefault();
                if (inventario == null) return RequestResult<InventarioDto>.CreateNoSuccess($"No existe el inventario con identificador {idInventario}");
                var resultado = new InventarioDto()
                {
                    Producto = inventario.Producto,
                    IdInventario = inventario.IdInventario,
                    Cantidad = inventario.Cantidad,
                    FechaVencimiento = inventario.FechaVencimiento,
                    Mecatos = inventario.Mecatos,
                    Id = inventario.Id,
                    IdUnidadMedida = inventario.IdUnidadMedida,
                    Stock = inventario.Stock
                };
                return RequestResult<InventarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<InventarioDto>.CreateError($"Ha ocurrido un error:{ex.Message}");
            }
        }

        public InventarioDto Update(InventarioDto inventario)
        {
            if (inventario == null) return null;
            if (inventario.Id.Equals(Guid.Empty)) return null;
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
                IdInventario = entidad.IdInventario,
                Cantidad = entidad.Cantidad,
                FechaVencimiento = entidad.FechaVencimiento,
                Mecatos = entidad.Mecatos,
                Id = entidad.Id,
                IdUnidadMedida = entidad.IdUnidadMedida,
                Stock = entidad.Stock
            };
        }
    }
}
