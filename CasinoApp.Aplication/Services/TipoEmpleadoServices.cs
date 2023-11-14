using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoEmpleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class TipoEmpleadoServices : ITipoEmpleadoServices
    {
        public RequestResult<TipoEmpleadoDto> Create(TipoEmpleadoDto tipoEmpleado)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idTipoEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<TipoEmpleadoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<TipoEmpleadoDto> GetById(int idTipoEmpleado)
        {
            throw new NotImplementedException();
        }

        public TipoEmpleadoDto Update(TipoEmpleadoDto tipoEmpleado)
        {
            throw new NotImplementedException();
        }
    }
}
