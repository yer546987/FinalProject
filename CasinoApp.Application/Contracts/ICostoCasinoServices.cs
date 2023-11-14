using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface ICostoCasinoServices
    {
        public CostoCasinoDto Update(CostoCasinoDto costoCasino);
        public bool Delete(int idCostoCasino);
        public RequestResult<List<CostoCasinoDto>> GetAll();
        RequestResult<CostoCasinoDto> Create(CostoCasinoDto costoCasino);
        RequestResult<CostoCasinoDto> GetById(int idCostoCasino);
    }
}
