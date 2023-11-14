using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
   public interface ITipoDocumentoServices
    {
        public TipoDocumentoDto Update(TipoDocumentoDto tipoDocumento);
        public bool Delete(int idTipoDocumento);
        public RequestResult<List<TipoDocumentoDto>> GetAll();
        public RequestResult<TipoDocumentoDto> GetById(Guid idTipoDocumento);
        RequestResult<TipoDocumentoDto> Create(TipoDocumentoDto tipoDocumento);
    }
}
