using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class GrupoEmpleadoServices : IGrupoEmpleadoServices
    {
        public RequestResult<GrupoEmpleadoDto> Create(GrupoEmpleadoDto grupoEmpleado)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idGrupoEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<GrupoEmpleadoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<GrupoEmpleadoDto> GetById(int idGrupoEmpleado)
        {
            throw new NotImplementedException();
        }

        public GrupoEmpleadoDto Update(GrupoEmpleadoDto grupoEmpleado)
        {
            throw new NotImplementedException();
        }
    }
}
