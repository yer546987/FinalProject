using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Producto
{
    public class ProductoDto
    {
        [Key]
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public DateTime FechaVencimientoProducto { get; set; }

        public int IdTipoProducto { get; set; }

    }
}
