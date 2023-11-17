using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CasinoApp.Aplication.Models;

public partial class TipoComida
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Nombre { get; set; }

    public double? Precio { get; set; }

    public string Descripcion { get; set; }

    public DateTime TiempoInicial { get; set; }

    public DateTime TiempoFinal { get; set; }   

    public int Limite { get; set; }

    public bool Cronograma { get; set; }

    [ForeignKey("ingredientes")]
    public int IdIngredientes { get; set; }

    public virtual ICollection<CostoCasino> CostoCasinos { get; set; } = new List<CostoCasino>();

    public virtual Ingrediente  ingredientes { get; set; }

    public virtual ICollection<MovimientoCasino> MovimientoCasinos { get; set; } = new List<MovimientoCasino>();
}
