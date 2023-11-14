using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ITipoEmpleadoServices
    {
        public TipoEmpleadoDto Update(TipoEmpleadoDto tipoEmpleado);
        public bool Delete(int idTipoEmpleado);

        public RequestResult<List<TipoEmpleadoDto>> GetAll();
        public RequestResult<TipoEmpleadoDto> GetById(int idTipoEmpleado);
        RequestResult<TipoEmpleadoDto> Create(TipoEmpleadoDto tipoEmpleado);
    }
}
