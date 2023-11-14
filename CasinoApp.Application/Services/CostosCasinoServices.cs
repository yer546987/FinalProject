using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Services
{
    public class CostosCasinoServices : ICostoCasinoServices
    {
        private CasinoAppContext _Context;
        public RequestResult<CostoCasinoDto> Create(CostoCasinoDto costoCasino)
        {
            try
            {
                if (costoCasino is null) return RequestResult<CostoCasinoDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(costoCasino.PrecioC)) return RequestResult<CostoCasinoDto>.CreateNoSuccess("El costo del casino es requerido");
                CostoCasino entity = new CostoCasino();
                entity.Precio = costoCasino.PrecioC;
                var result = _Context.CostoCasinos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0) return RequestResult<CostoCasinoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el costo de casino");
                var resultado = new CostoCasinoDto() 
                {
                    IdCostoCasino = result.Entity.Id,
                    IdGrupoEmpleado = result.Entity.IdGrupoEmpleado,
                    IdTipoComida = result.Entity.IdTipoComida
                };
                return RequestResult<CostoCasinoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<CostoCasinoDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idCostoCasino)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<CostoCasinoDto>> GetAll()
        {
            try
            {
                var costoCasino = _Context.CostoCasinos.ToList();
                List<CostoCasinoDto> result = new List<CostoCasinoDto>();
                foreach (var item in costoCasino)
                {
                    result.Add(new CostoCasinoDto() 
                    {
                        IdCostoCasino = item.Id,
                        IdGrupoEmpleado = item.IdGrupoEmpleado,
                        IdTipoComida = item.IdTipoComida
                    });
                }
                return RequestResult<List<CostoCasinoDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RequestResult<CostoCasinoDto> GetById(int idCostoCasino)
        {
            try
            {
                var costoCasino = _Context.CostoCasinos.Where(x => x.Id == idCostoCasino).FirstOrDefault();
                if (costoCasino is null) return RequestResult<CostoCasinoDto>.CreateNoSuccess($"No existe el costo de casino con {idCostoCasino}");
                var resultados = new CostoCasinoDto()
                {
                    IdCostoCasino = costoCasino.Id,
                    IdGrupoEmpleado = costoCasino.IdGrupoEmpleado,
                    IdTipoComida = costoCasino.IdTipoComida
                };
                return RequestResult<CostoCasinoDto>.CreateSuccess(resultados);
            }
            catch (Exception ex)
            {
                return RequestResult<CostoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public CostoCasinoDto Update(CostoCasinoDto costoCasino)
        {
            if (costoCasino is null)return null;
            if (costoCasino.IdCostoCasino is 0)return null;
            if (string.IsNullOrEmpty(costoCasino.PrecioC))return null;
            var entity = _Context.CostoCasinos
                .Where(x => x.Id.Equals(costoCasino.IdCostoCasino))
                .FirstOrDefault();
            if (entity == null) return null;
            entity.Precio = costoCasino.PrecioC;
            _Context.Attach(entity);
            _Context.Entry(entity).State= EntityState.Modified;
            _Context.SaveChanges();
            return new CostoCasinoDto() 
            {
                IdCostoCasino = entity.Id,
                IdGrupoEmpleado = entity.IdGrupoEmpleado,
                IdTipoComida = entity.IdTipoComida
            };
        }
    }
}
