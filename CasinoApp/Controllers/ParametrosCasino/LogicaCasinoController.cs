using CasinoApp.Aplication.Services;
using CasinoApp.Entities.CrearMovimiento;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.TipoComida;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogicaCasinoController : Controller
    {
        private LogicaCasinoService services;

        public LogicaCasinoController()
        {
            services = new LogicaCasinoService();
        }

        /// <summary>
        /// Este metodo retorna todas los Tipo de comidas registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<TipoComidaDto>> GetAll()
        {
            return services.GetAll();
        }


        [HttpPost]
        public RequestResult<MovimientoCasinoDto> Create(CrearMovimiento crearMovimiento)
        {
          
            if (crearMovimiento.Identificacion != 0 && crearMovimiento.IdTipoComida != 0)
            {
                return services.Create(crearMovimiento.Identificacion, crearMovimiento.IdTipoComida);
            }

            return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("El identificador o el tipo de comida llegaron vacíos");
        }
    }
}
