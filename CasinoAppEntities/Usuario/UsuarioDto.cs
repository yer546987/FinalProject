using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.Usuario
{
    public class UsuarioDto
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Usuario1 { get; set; }
    }
}
