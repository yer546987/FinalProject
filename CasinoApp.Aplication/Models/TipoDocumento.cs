using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string TipoIdentificacion { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
