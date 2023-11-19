using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoComida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface ITipoProductosSeervices
    {
        public RequestResult<TipoProductoDto> Update(TipoProductoDto tipoProducto);
        public bool Delete(int Idtipo);
        public RequestResult<List<TipoProductoDto>> GetAll();
        public RequestResult<TipoProductoDto> GetById(int Id);
        public RequestResult<TipoProductoDto> Create(TipoProductoDto tipoProducto);
    }
}
