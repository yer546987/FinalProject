using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.UnidadMedida
{
    public class UnidadMedidaDto
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
