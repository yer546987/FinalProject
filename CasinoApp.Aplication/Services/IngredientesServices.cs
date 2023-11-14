using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class IngredientesServices : IIngredientesServices
    {
        private CasinoAppContext _Context;

        public IngredientesServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<IngredientesDto> Create(IngredientesDto ingredientes)
        {
            try
            {
                if (ingredientes is null)
                    return RequestResult<IngredientesDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(ingredientes.Cantidad))
                    return RequestResult<IngredientesDto>.CreateNoSuccess("La cantidad de ingredientes es requerido");
                Ingrediente entity = new Ingrediente();
                entity.Cantidad = ingredientes.Cantidad;
                var result = _Context.Ingredientes.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<IngredientesDto>.CreateNoSuccess("Ha ocurrido un error al registrar el ingrediente.");
                var resultado = new IngredientesDto()
                {
                    Cantidad = result.Entity.Cantidad,
                    Id = result.Entity.Id,
                    IdInventario = result.Entity.IdInventario,
                    IdUnidadPesaje = result.Entity.IdUnidadPesaje
                };
                return RequestResult<IngredientesDto>.CreateSuccess(resultado);

            }
            catch (Exception ex)
            {
                return RequestResult<IngredientesDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idIngredientes)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<IngredientesDto>> GetAll()
        {
            try
            {
                var ingredientes = _Context.Ingredientes.ToList();
                List<IngredientesDto> result = new List<IngredientesDto>();
                foreach (var item in ingredientes)
                {
                    result.Add(new IngredientesDto()
                    {
                        Cantidad = item.Cantidad,
                        Id = item.Id,
                        IdInventario = item.IdInventario,
                        IdUnidadPesaje = item.IdUnidadPesaje
                    });
                }
                return RequestResult<List<IngredientesDto>>.CreateSuccess(result);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<IngredientesDto> GetById(int idIngredientes)
        {
            try
            {
                var ingredientes = _Context.Ingredientes.Where(x => x.Id == idIngredientes).FirstOrDefault();
                if (ingredientes is null) return RequestResult<IngredientesDto>.CreateNoSuccess($"No existe el ingrediente con identificador {idIngredientes}");
                var resultado = new IngredientesDto()
                {
                    Cantidad = ingredientes.Cantidad,
                    Id = ingredientes.Id,
                    IdInventario = ingredientes.IdInventario,
                    IdUnidadPesaje = ingredientes.IdUnidadPesaje
                };
                return RequestResult<IngredientesDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<IngredientesDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public IngredientesDto Update(IngredientesDto ingredientes)
        {
            if (ingredientes is null) return null;
            if (ingredientes.Id is 0) return null;
            if (string.IsNullOrEmpty(ingredientes.Cantidad)) return null;
            var entidad = _Context.Ingredientes
                                .Where(x => x.Id.Equals(ingredientes.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Cantidad = ingredientes.Cantidad;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new IngredientesDto()
            {
                Cantidad = entidad.Cantidad,
                Id = entidad.Id,
                IdInventario = entidad.IdInventario,
                IdUnidadPesaje = entidad.IdUnidadPesaje
            };
        }
    }
}
