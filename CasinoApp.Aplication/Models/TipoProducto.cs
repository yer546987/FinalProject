using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class TipoProducto
{
    public int Id { get; set; }

    public string Tipo { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
