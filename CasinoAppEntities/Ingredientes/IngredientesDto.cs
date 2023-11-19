using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Ingredientes
{
    public class IngredientesDto
    {
        [Key]
        public int Id { get; set; }
        public int IdTipoComida { get; set; }
        public double Cantidad { get; set; }
        public int IdProducto { get; set; }

    }
}
