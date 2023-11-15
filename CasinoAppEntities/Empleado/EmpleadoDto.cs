using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoEmpleado;
using System.ComponentModel.DataAnnotations;

namespace CasinoApp.Entities.Empleado
{
    public class EmpleadoDto
    {
        [Key]
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoE { get; set; }
        public int IdTipoIdentificacionE { get; set; }
        public int IdentificacionE { get; set; }
        public int IdTipoEmpleadoE { get; set; }
        public int IdGrupoEE { get; set; }
        public bool InternoE { get; set; }
        
    }
}
