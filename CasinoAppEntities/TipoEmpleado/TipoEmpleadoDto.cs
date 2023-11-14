﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.TipoEmpleado
{
    public class TipoEmpleadoDto
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
