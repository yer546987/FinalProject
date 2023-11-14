using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IIngredientesServices
    {
        public IngredientesDto Update(IngredientesDto ingredientes);
        public bool Delete(int idIngredientes);
        public RequestResult<List<IngredientesDto>> GetAll();
        public RequestResult<IngredientesDto> GetById(Guid idIngredientes);
        RequestResult<IngredientesDto> Create(IngredientesDto ingredientes);
    }
}
