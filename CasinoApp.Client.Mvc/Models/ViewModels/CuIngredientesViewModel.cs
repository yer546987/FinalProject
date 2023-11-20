using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.TipoComida;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CuIngredientesViewModel
    {
        public IngredientesDto Ingredientes { get; set; }
        public List<ProductoDto> Productos { get; set; }
        public List<TipoComidaDto> TipoComidas { get; set; }
    }
}
