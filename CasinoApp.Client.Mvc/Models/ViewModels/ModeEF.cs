using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class ModeEF
    {
        public EmpleadoDto empleado { get; set; }
        public MovimientoCasinoDto movimientoCasino { get; set; }
        public TipoComidaDto tipoComida { get; set; }
        public List<TipoComidaDto> ListTipoComida { get; set; }
        public int TipoComida { get; set; }
    }
}
