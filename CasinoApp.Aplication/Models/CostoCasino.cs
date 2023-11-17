using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class CostoCasino
{
    public int Id { get; set; }
    public double? Precio { get; set; }

    [ForeignKey("TipoComida")]
    public int IdTipoComida { get; set; }

    [ForeignKey("GrupoEmpleado")]
    public int IdGrupoEmpleado { get; set; }

    public virtual GrupoEmpleado GrupoEmpleado { get; set; }

    public virtual TipoComida TipoComida { get; set; }
}
