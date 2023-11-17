using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoEmpleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ITipoDocumentoServices
    {
        public RequestResult<TipoDocumentoDto> Update(TipoDocumentoDto tipoDocumento);
        public bool Delete(int idTipoDocumento);

        public RequestResult<List<TipoDocumentoDto>> GetAll();
        public RequestResult<TipoDocumentoDto> GetById(int idtipoDocumento);
        RequestResult<TipoDocumentoDto> Create(TipoDocumentoDto tipoDocumento);
    }
}
