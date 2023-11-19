using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    public int IdTipoComida { get; set; }

    public int IdProducto { get; set; }

    public double Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }

    public virtual TipoComida IdTipoComidaNavigation { get; set; }
}
