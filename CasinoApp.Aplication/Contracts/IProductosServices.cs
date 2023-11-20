using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Contracts
{
    public interface IProductosServices
    {
        public RequestResult<ProductoDto> Update(ProductoDto producto);
        public bool Delete(int idProducto);

        public RequestResult<List<ProductoDto>> GetAll();
        public RequestResult<ProductoDto> GetById(int idProducto);
        RequestResult<ProductoDto> Create(ProductoDto producto);
    }
}
