using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class CostoCasino
{
    public int Id { get; set; }

    public double? Precio { get; set; }

    public int IdTipoComida { get; set; }

    public int IdGrupoEmpleado { get; set; }

    public virtual GrupoEmpleado IdGrupoEmpleadoNavigation { get; set; }

    public virtual TipoComidum IdTipoComidaNavigation { get; set; }
}
