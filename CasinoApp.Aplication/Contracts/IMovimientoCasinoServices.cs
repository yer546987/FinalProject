using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface IMovimientoCasinoServices
    {
        public RequestResult<MovimientoCasinoDto> Update(MovimientoCasinoDto movimientoCasino);
        public bool Delete(int idMovimientoCasino);

        public RequestResult<List<MovimientoCasinoDto>> GetAll();
        public RequestResult<MovimientoCasinoDto> GetById(int idMovimientoCasino);
        RequestResult<MovimientoCasinoDto> Create(MovimientoCasinoDto movimientoCasino);
       
    }
}
