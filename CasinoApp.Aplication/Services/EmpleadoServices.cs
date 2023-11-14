using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class EmpleadoServices : IEmpleadosServices
    {
        public RequestResult<EmpleadoDto> Create(EmpleadoDto empleado)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idEmpleado)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<EmpleadoDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<EmpleadoDto> GetById(int idEmpleado)
        {
            throw new NotImplementedException();
        }

        public EmpleadoDto Update(EmpleadoDto empleado)
        {
            throw new NotImplementedException();
        }
    }
}
