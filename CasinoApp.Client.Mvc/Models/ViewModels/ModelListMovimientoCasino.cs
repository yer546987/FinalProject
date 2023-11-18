using System.Collections.Generic;
using System;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.MovimientoCasino;

namespace CasinoApp.Client.Mvc.Models.ViewModels
{
    public class ModelListMovimientoCasino
    {
        public DateTime Fechainicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int IdTipoComida { get; set; }
        public List<TipoComidaDto> ListTipoComida  { get; set; }
        public List<MovimientoCasinoDto> ListMovimientoCasino { get; set; }
        public int TipoComida { get; internal set; }
    }
}
