using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class MovimientoCasino
{
    public int Id { get; set; }

    public double? Costo { get; set; }

    public int IdTipoComida { get; set; }

    public int IdGrupoEmpleado { get; set; }

    public DateTime HoraRegistro { get; set; }

    public int IdEmpleado { get; set; }

    public virtual Empleado IdEmpleadoNavigation { get; set; }

    public virtual GrupoEmpleado IdGrupoEmpleadoNavigation { get; set; }

    public virtual TipoComidum IdTipoComidaNavigation { get; set; }
}
