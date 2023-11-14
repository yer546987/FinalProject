using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Ingredientes
{
    public class IngredientesDto
    {
        public int Id { get; set; }
        public int IdUnidadPesaje { get; set; }
        public string Cantidad { get; set; }
        public int IdInventario { get; set; }
        public string NombreUnidadPesaje { get; set; }

    }
}
