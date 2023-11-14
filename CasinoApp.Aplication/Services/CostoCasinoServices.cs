using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class CostoCasinoServices : ICostoCasinoServices
    {
        public RequestResult<CostoCasinoDto> Create(CostoCasinoDto costoCasino)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idConstoCasino)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<CostoCasinoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<CostoCasinoDto> GetById(int idCostoCasino)
        {
            throw new NotImplementedException();
        }

        public CostoCasinoDto Update(CostoCasinoDto costoCasino)
        {
            throw new NotImplementedException();
        }
    }
}
