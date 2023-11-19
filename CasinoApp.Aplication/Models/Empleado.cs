using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public int IdTipoIdentificacion { get; set; }

    public int Identificacion { get; set; }

    public int IdTipoEmpleado { get; set; }

    public int IdGrupoE { get; set; }

    public bool Interno { get; set; }

    public virtual GrupoEmpleado IdGrupoENavigation { get; set; }

    public virtual TipoEmpleado IdTipoEmpleadoNavigation { get; set; }

    public virtual TipoDocumento IdTipoIdentificacionNavigation { get; set; }

    public virtual ICollection<MovimientoCasino> MovimientoCasinos { get; set; } = new List<MovimientoCasino>();
}
