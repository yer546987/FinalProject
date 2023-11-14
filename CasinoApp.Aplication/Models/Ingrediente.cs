using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    public int IdUnidadPesaje { get; set; }

    public string Cantidad { get; set; }

    public int IdInventario { get; set; }

    public virtual UnidadMedidum IdInventarioNavigation { get; set; }

    public virtual ICollection<TipoComidum> TipoComida { get; set; } = new List<TipoComidum>();
}
