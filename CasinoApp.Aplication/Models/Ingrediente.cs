using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    [ForeignKey("TipoComida")]
    public int IdTipoComida { get; set; }

    [ForeignKey("Producto")]
    public int IdProducto { get; set; }

    public double Cantidad { get; set; }

    public virtual Producto Producto { get; set; }

    public virtual TipoComida TipoComida { get; set; }
}
