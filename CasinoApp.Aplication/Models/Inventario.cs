using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class Inventario
{
    public int Id { get; set; }

    [ForeignKey("Producto")]
    public int IdProducto { get; set; }

    public double Cantidad { get; set; }

    public int Stock { get; set; }

    public virtual Producto Producto { get; set; }
}
