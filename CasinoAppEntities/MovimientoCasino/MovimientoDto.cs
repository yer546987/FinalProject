using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.MovimientoCasino
{
    public class MovimientoCasinoDto
    {
        public int IdMovimientoCasino { get; set; }
        public double? Costo { get; set; }
        public int IdTipoComida { get; set; }
        public int IdGrupoEmpleado { get; set; }
        public DateTime HoraRegistro { get; set; }
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string NombreGrupoEmpleado { get; set; }
        public string NombreTipoComida { get; set; }
    }
}
