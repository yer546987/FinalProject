using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface IGrupoEmpleadoServices
    {
        public GrupoEmpleadoDto Update(GrupoEmpleadoDto grupoEmpleado);
        public bool Delete(int idGrupoEmpleado);

        public RequestResult<List<GrupoEmpleadoDto>> GetAll();
        public RequestResult<GrupoEmpleadoDto> GetById(int idGrupoEmpleado);
        RequestResult<GrupoEmpleadoDto> Create(GrupoEmpleadoDto grupoEmpleado);
    }
}
