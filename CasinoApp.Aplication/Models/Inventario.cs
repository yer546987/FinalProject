using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public double Cantidad { get; set; }

    public int Stock { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }
}
