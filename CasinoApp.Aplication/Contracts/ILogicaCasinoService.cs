using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ILogicaCasinoService
    {
        public RequestResult<List<TipoComidaDto>> GetAll();
       
        RequestResult<MovimientoCasinoDto> Create(int Identificacion, int idTipocomida);
    }
}
