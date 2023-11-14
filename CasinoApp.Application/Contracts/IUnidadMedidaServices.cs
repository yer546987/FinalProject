using CasinoApp.Entities.Http;
using CasinoApp.Entities.UnidadMedida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IUnidadMedidaServices
    {
        public UnidadMedidaDto Update(UnidadMedidaDto unidadMedida);
        public bool Delete(int idUnidadMedida);
        public RequestResult<List<UnidadMedidaDto>> GetAll();
        public RequestResult<UnidadMedidaDto> GetById(Guid idUnidadMedida);
        RequestResult<UnidadMedidaDto> Create(UnidadMedidaDto unidadMedida);
    }
}
