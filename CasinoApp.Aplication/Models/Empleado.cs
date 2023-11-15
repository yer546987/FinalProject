using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }
    [ForeignKey("TipoDocumento")]
    public int IdTipoIdentificacion { get; set; }
    public int Identificacion { get; set; }
    [ForeignKey("TipoEmpleado")]
    public int IdTipoEmpleado { get; set; }
    [ForeignKey("GrupoEmpleado")]
    public int IdGrupoE { get; set; }

    public bool Interno { get; set; }

    public virtual GrupoEmpleado GrupoE { get; set; }

    public virtual TipoEmpleado TipoEmpleado { get; set; }

    public virtual TipoDocumento TipoDocumento{ get; set; }

    public virtual ICollection<MovimientoCasino> MovimientoCasinos { get; set; } = new List<MovimientoCasino>();
}
