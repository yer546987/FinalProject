using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.TipoEmpleado;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEmpleadoController : ControllerBase
    {
        private TipoEmpleadoServices services;

        public TipoEmpleadoController() 
        {
            services = new TipoEmpleadoServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<TipoEmpleadoDto>> GetAll()
        {
            return services.GetAll();
        }


        /// <summary>
        /// Este metodo retorna una especie por id
        /// </summary>
        /// <param name="id">Identificador de la especie</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public RequestResult<TipoEmpleadoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<TipoEmpleadoDto> Create([FromBody] TipoEmpleadoDto tipoEmpleado)
        {
            if (ModelState.IsValid)
            {
                return services.Create(tipoEmpleado);
            }
            return RequestResult<TipoEmpleadoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public TipoEmpleadoDto Update([FromBody] TipoEmpleadoDto tipoEmpleado)
        {
            if (ModelState.IsValid)
            {
                return services.Update(tipoEmpleado);
            }
            return null;
        }
    }
}
