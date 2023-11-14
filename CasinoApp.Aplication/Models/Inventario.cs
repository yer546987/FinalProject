using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public string Producto { get; set; }

    public DateTime FechaVencimiento { get; set; }

    public int Stock { get; set; }

    public int IdUnidadMedida { get; set; }

    public double Cantidad { get; set; }

    public string Mecatos { get; set; }

    public int IdInventario { get; set; }

    public virtual UnidadMedidum IdUnidadMedidaNavigation { get; set; }
}
