using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface IEmpleadosServices
    {
        public EmpleadoDto Update(EmpleadoDto empleado);
        public bool Delete(int idEmpleado);

        public RequestResult<List<EmpleadoDto>> GetAll();
        public RequestResult<EmpleadoDto> GetById(int idEmpleado);
        RequestResult<EmpleadoDto> Create(EmpleadoDto empleado);
    }
}
