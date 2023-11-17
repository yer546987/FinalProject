using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoEmpleado;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CUCostoCasinoViewModels
    {
        public CostoCasinoDto CostoCasino { get; set; }
        public List<TipoComidaDto> TipoComida { get; set; }
        public List<GrupoEmpleadoDto> GruposEmpleado { get; set; }
    }
}
