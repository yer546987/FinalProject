using CasinoApp.Entities.Inventario;
using CasinoApp.Entities.Producto;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CuInventarioViewModel
    {
        public InventarioDto Inventario { get; set; }

        public List<ProductoDto> Producto { get; set; }
    }
}
