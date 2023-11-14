using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class InventarioServices : IInventarioServices
    {
        public RequestResult<InventarioDto> Create(InventarioDto inventario)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int idinventario)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<InventarioDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<InventarioDto> GetById(int idinventario)
        {
            throw new NotImplementedException();
        }

        public InventarioDto Update(InventarioDto inventario)
        {
            throw new NotImplementedException();
        }
    }
}
