using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ICostoCasinoServices
    {
        public CostoCasinoDto Update(CostoCasinoDto costoCasino);
        public bool Delete(int idConstoCasino);

        public RequestResult<List<CostoCasinoDto>> GetAll();
        public RequestResult<CostoCasinoDto> GetById(int idCostoCasino);
        RequestResult<CostoCasinoDto> Create(CostoCasinoDto costoCasino);
    }
}
