using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.CostoCasino
{
    public class CostoCasinoDto
    {
        public int IdCostoCasino { get; set; }
        public double? PrecioC { get; set; }
        public int IdTipoComida { get; set; }
        public int IdGrupoEmpleado { get; set; }
        public string NombreGrupoEmpleado { get; set; }
        public string NombreTipoComida { get; set; }
    }
}
