using CasinoApp.Application.Services;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoEmpleadoControllers : ControllerBase
    {
        private GrupoEmpleadoServices services;

        public GrupoEmpleadoControllers() 
        {
            services = new GrupoEmpleadoServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<GrupoEmpleadoDto>> GetAll()
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
        public RequestResult<GrupoEmpleadoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<GrupoEmpleadoDto> Create([FromBody] GrupoEmpleadoDto grupoEmpleado)
        {
            if (ModelState.IsValid)
            {
                return services.Create(grupoEmpleado);
            }
            return RequestResult<GrupoEmpleadoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public GrupoEmpleadoDto Update([FromBody] GrupoEmpleadoDto grupoEmpleado)
        {
            if (ModelState.IsValid)
            {
                return services.Update(grupoEmpleado);
            }
            return null;
        }
    }
}
