﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Inventario
{
    public class InventarioDto
    {
        [Key]
        public int Id { get; set; }

        public int IdProducto { get; set; }      

        public int Stock { get; set; }
      
        public double Cantidad { get; set; }


    }
}
