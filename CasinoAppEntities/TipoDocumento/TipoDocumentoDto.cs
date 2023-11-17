using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Entities.TipoDocumento
{
    public class TipoDocumentoDto
    {
        [Key]
        public int Id { get; set; }

        public string TipoIdentificacion { get; set; }
    }
}
