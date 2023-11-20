using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public DateTime FechaVencimiento { get; set; }

    [ForeignKey("TipoProducto")]
    public int IdTipoProducto { get; set; }

    public virtual TipoProducto TipoProducto { get; set; }

    public virtual ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
