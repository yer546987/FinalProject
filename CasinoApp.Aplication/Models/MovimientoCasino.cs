using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class MovimientoCasino
{
    [Key]
    public int Id { get; set; }

    public double? Costo { get; set; }


    [ForeignKey("TipoComida")]
    public int IdTipoComida { get; set; }

    [ForeignKey("GrupoEmpleado")]
    public int IdGrupoEmpleado { get; set; }

    public DateTime HoraRegistro { get; set; }


    [ForeignKey("Empleado")]
    public int IdEmpleado { get; set; }

    public virtual Empleado empleado { get; set; }

    public virtual GrupoEmpleado grupoempleado { get; set; }

    public virtual TipoComida tipoComida { get; set; }
}
