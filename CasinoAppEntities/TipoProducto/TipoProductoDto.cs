using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Producto
{
    public class TipoProductoDto
    {
        [Key]
        public int IdTipoProducto { get; set; }
        public string TipoProducto { get; set; }
    }
}
