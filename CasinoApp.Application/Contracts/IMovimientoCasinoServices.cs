using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IMovimientoCasinoServices
    {
        public MovimientoCasinoDto Update(MovimientoCasinoDto movimientoCasino);
        public bool Delete(int idMovimientoCasino);
        public RequestResult<List<MovimientoCasinoDto>> GetAll();
        public RequestResult<MovimientoCasinoDto> GetById(int idMovimientoCasino);
        RequestResult<MovimientoCasinoDto> Create(MovimientoCasinoDto movimientoCasino);
    }
}
