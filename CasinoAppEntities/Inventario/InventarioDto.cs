using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Inventario
{
    public class InventarioDto
    {
        public Guid Id { get; set; }

        public string Producto { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int Stock { get; set; }

        public Guid IdUnidadMedida { get; set; }

        public double Cantidad { get; set; }

        public string Mecatos { get; set; }

        public Guid IdInventario { get; set; }

    }
}
