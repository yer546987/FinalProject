using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class TipoEmpleado
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
