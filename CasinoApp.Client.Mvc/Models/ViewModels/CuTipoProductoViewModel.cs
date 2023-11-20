using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoComida;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CuTipoProductoViewModel
    {
        public ProductoDto producto { get; set; }
        public List<TipoProductoDto> TipoProducto { get; set; }
    }
}
