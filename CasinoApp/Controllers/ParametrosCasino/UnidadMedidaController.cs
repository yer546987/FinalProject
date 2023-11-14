using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.UnidadMedida;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : Controller
    {
        private UnidadMedidaServices services;

        public UnidadMedidaController()
        {
            services = new UnidadMedidaServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<UnidadMedidaDto>> GetAll()
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
        public RequestResult<UnidadMedidaDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<UnidadMedidaDto> Create([FromBody] UnidadMedidaDto unidadMedida)
        {
            if (ModelState.IsValid)
            {
                return services.Create(unidadMedida);
            }
            return RequestResult<UnidadMedidaDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public UnidadMedidaDto Update([FromBody] UnidadMedidaDto unidadMedida)
        {
            if (ModelState.IsValid)
            {
                return services.Update(unidadMedida);
            }
            return null;
        }
    }
}
