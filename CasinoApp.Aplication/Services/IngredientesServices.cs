using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class IngredientesServices : IIngredientesServices
    {
        public RequestResult<IngredientesDto> Create(IngredientesDto ingredientes)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idIngredientes)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<IngredientesDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<IngredientesDto> GetById(int idIngredientes)
        {
            throw new NotImplementedException();
        }

        public IngredientesDto Update(IngredientesDto ingredientes)
        {
            throw new NotImplementedException();
        }
    }
}
