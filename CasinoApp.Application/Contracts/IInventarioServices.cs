using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IInventarioServices
    {
        public InventarioDto Update(InventarioDto inventario);
        public bool Delete(int idInventario);
        public RequestResult<List<InventarioDto>> GetAll();
        public RequestResult<InventarioDto> GetById(Guid idInventario);
        RequestResult<InventarioDto> Create(InventarioDto inventario);
    }
}
