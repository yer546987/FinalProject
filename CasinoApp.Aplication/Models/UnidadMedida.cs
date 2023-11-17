using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class UnidadMedida
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
