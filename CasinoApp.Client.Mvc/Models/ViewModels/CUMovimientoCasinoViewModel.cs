using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CUMovimientoCasinoViewModel
    {
        public MovimientoCasinoDto movimientoCasino { get; set; }

        public List<TipoComidaDto> tipoComida { get; set; }

        public List<EmpleadoDto> empleado { get; set; }
        public List<GrupoEmpleadoDto> grupoEmpleados { get; set; }
    }
}
