using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Empleado
{
    public class EmpleadoDto
    {
        [Key]
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoE { get; set; }
        public int IdTipoIdentificacionE { get; set; }
        public decimal IdentificacionE { get; set; }
        public int IdTipoEmpleadoE { get; set; }
        public int IdGrupoEE { get; set; }
        public bool InternoE { get; set; }
        public string NombreGrupoEmpleadoE { get; set; }

        public string NombreTipoEmpleadoE { get; set; }

        public string NombreTipoIdentificacionE { get; set; }

    }
}
