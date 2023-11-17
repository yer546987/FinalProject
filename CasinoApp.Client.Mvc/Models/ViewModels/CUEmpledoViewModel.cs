using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoEmpleado;
using System.Collections.Generic;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class CUEmpledoViewModel
    {
        public EmpleadoDto Empleado { get; set; }
        public List<TipoEmpleadoDto> TiposEmpleado { get; set; }
        public List<GrupoEmpleadoDto> GruposEmpleado { get; set; }
        public List<TipoDocumentoDto> TiposIdentificacion { get; set; }
    }
}
