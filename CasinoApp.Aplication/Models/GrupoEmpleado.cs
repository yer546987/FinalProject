using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class GrupoEmpleado
{
    public int Id { get; set; }

    public string NombreGrupo { get; set; }

    public virtual ICollection<CostoCasino> CostoCasinos { get; set; } = new List<CostoCasino>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<MovimientoCasino> MovimientoCasinos { get; set; } = new List<MovimientoCasino>();
}
