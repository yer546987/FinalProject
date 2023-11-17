using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    [ForeignKey("UnidadMedida")]
    public int IdUnidadPesaje { get; set; }

    public string Cantidad { get; set; }

    [ForeignKey("Inventario")]
    public int IdInventario { get; set; }

    public virtual UnidadMedida UnidadMedida { get; set; }

    public virtual Inventario Inventario { get; set; }

    public virtual ICollection<TipoComida> TipoComida { get; set; } = new List<TipoComida>();
}
