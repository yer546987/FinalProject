﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.GrupoEmpleado
{
    public class GrupoEmpleadoDto
    {
        [Key]
        public int Id { get; set; }

        public string NombreGrupo { get; set; }
    }
}
