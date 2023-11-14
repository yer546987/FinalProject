using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class MovimientoCasinoServices : IMovimientoCasinoServices
    {
        public RequestResult<MovimientoCasinoDto> Create(MovimientoCasinoDto especie)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idMovimientoCasino)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<MovimientoCasinoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<MovimientoCasinoDto> GetById(int idMovimientoCasino)
        {
            throw new NotImplementedException();
        }

        public MovimientoCasinoDto Update(MovimientoCasinoDto especie)
        {
            throw new NotImplementedException();
        }
    }
}
