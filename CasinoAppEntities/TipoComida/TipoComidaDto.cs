using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.TipoComida
{
    public class TipoComidaDto
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public double? Precio { get; set; }

        public string Descripcion { get; set; }

        public DateTime TiempoInicial { get; set; }

        public DateTime TiempoFinal { get; set; }

        public int Limite { get; set; }

        public bool Cronograma { get; set; }

   
    }
}
