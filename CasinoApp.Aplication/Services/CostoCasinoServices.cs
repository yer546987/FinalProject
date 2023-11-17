using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class CostoCasinoServices : ICostoCasinoServices
    {
        private CasinoAppContext _Context;

        public CostoCasinoServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<CostoCasinoDto> Create(CostoCasinoDto costoCasino)
        {
            try
            {
                if (costoCasino is null)
                    return RequestResult<CostoCasinoDto>.CreateNoSuccess("Los datos son requeridos");
                CostoCasino entity = new CostoCasino();
                entity.Precio = costoCasino.PrecioC;
                entity.IdGrupoEmpleado = costoCasino.IdGrupoEmpleado;
                entity.IdTipoComida = costoCasino.IdTipoComida;
                var result = _Context.CostoCasinos.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<CostoCasinoDto>.CreateNoSuccess("Ha ocurrido un error al registrar el costo del casino.");
                var resultado = new CostoCasinoDto()
                {
                    PrecioC = result.Entity.Precio,
                    IdTipoComida = result.Entity.IdTipoComida,
                    IdGrupoEmpleado = result.Entity.IdGrupoEmpleado

                };
                return RequestResult<CostoCasinoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<CostoCasinoDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idConstoCasino)
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
                        PrecioC = item.Precio,
                        IdTipoComida = item.IdTipoComida,
                        IdGrupoEmpleado = item.IdGrupoEmpleado
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
                if (costoCasino is null) return RequestResult<CostoCasinoDto>.CreateNoSuccess($"No existe el costo de casino con identificador {idCostoCasino}");
                var resultado = new CostoCasinoDto()
                {
                    IdCostoCasino = costoCasino.Id,
                    PrecioC = costoCasino.Precio,
                    IdTipoComida = costoCasino.IdTipoComida,
                    IdGrupoEmpleado = costoCasino.IdGrupoEmpleado
                };
                return RequestResult<CostoCasinoDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<CostoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RequestResult<CostoCasinoDto> Update(CostoCasinoDto costoCasino)
        {
            try
            {
                if (costoCasino == null || costoCasino.IdCostoCasino == 0)
                {
                    return RequestResult<CostoCasinoDto>.CreateNoSuccess("Datos incorrectos para la actualización.");
                }

                var entidad = _Context.CostoCasinos
                                      .Where(x => x.Id.Equals(costoCasino.IdCostoCasino))
                                      .FirstOrDefault();

                if (entidad == null)
                {
                    return RequestResult<CostoCasinoDto>.CreateNoSuccess("CostoCasino no encontrado para la actualización.");
                }

                entidad.Precio = costoCasino.PrecioC;

                _Context.Attach(entidad);
                _Context.Entry(entidad).State = EntityState.Modified;
                _Context.SaveChanges();

                return RequestResult<CostoCasinoDto>.CreateSuccess(new CostoCasinoDto
                {
                    PrecioC = entidad.Precio,
                    IdTipoComida = entidad.IdTipoComida,
                    IdGrupoEmpleado = entidad.IdGrupoEmpleado,
                    IdCostoCasino = entidad.Id
                });
            }
            catch (Exception ex)
            {
                return RequestResult<CostoCasinoDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

    }
}
