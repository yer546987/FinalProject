using System;
using System.Collections.Generic;

namespace CasinoApp.Aplication.Models;

public partial class TipoComidum
{
    public int Id { get; set; }

    public string Nombre { get; set; }

    public double? Precio { get; set; }

    public string Descripcion { get; set; }

    public DateTime TiempoInicial { get; set; }

    public DateTime TiempoFinal { get; set; }

    public int Limite { get; set; }

    public bool Cronograma { get; set; }

    public int IdIngredientes { get; set; }

    public virtual ICollection<CostoCasino> CostoCasinos { get; set; } = new List<CostoCasino>();

    public virtual Ingrediente IdIngredientesNavigation { get; set; }

    public virtual ICollection<MovimientoCasino> MovimientoCasinos { get; set; } = new List<MovimientoCasino>();
}
