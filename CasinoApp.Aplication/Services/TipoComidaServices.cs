using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class TipoComidaServices : ITipoComidaServices
    {
        public RequestResult<TipoComidaDto> Create(TipoComidaDto tipoComida)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idTípoComida)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoComidaDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<TipoComidaDto> GetById(int idTipoComida)
        {
            throw new NotImplementedException();
        }

        public TipoComidaDto Update(TipoComidaDto tipoComida)
        {
            throw new NotImplementedException();
        }
    }
}
