using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.TipoComida;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CUTipoComidaViewModel
    {
        public TipoComidaDto tipoComida { get; set; }
        public List<IngredientesDto> ingredientes { get; set; }

    }
}
