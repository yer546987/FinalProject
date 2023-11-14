using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class EmpleadoController : Controller
    {
        private EmpleadoServices services;
        public EmpleadoController()
        {
            services = new EmpleadoServices();
        }


        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<EmpleadoDto>> GetAll()
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
        public RequestResult<EmpleadoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<EmpleadoDto> Create([FromBody] EmpleadoDto empleado)
        {
            if (ModelState.IsValid)
            {
                return services.Create(empleado);
            }
            return RequestResult<EmpleadoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public EmpleadoDto Update([FromBody] EmpleadoDto especie)
        {
            if (ModelState.IsValid)
            {
                return services.Update(especie);
            }
            return null;
        }
    }
}
