using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class TipoDocumentoServices : ITipoDocumentoServices
    {
        public RequestResult<TipoDocumentoDto> Create(TipoDocumentoDto tipoDocumento)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idTipoDocumento)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoDocumentoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<TipoDocumentoDto> GetById(int idtipoDocumento)
        {
            throw new NotImplementedException();
        }

        public TipoDocumentoDto Update(TipoDocumentoDto tipoDocumento)
        {
            throw new NotImplementedException();
        }
    }
}
