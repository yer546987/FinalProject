using CasinoApp.Application.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioControllers : ControllerBase
    {
        private InventarioServices services;

        public InventarioControllers() 
        {
            services = new InventarioServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<InventarioDto>> GetAll()
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
        public RequestResult<InventarioDto> GetById([FromRoute] Guid id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<InventarioDto> Create([FromBody] InventarioDto inventario)
        {
            if (ModelState.IsValid)
            {
                return services.Create(inventario);
            }
            return RequestResult<InventarioDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public InventarioDto Update([FromBody] InventarioDto inventario)
        {
            if (ModelState.IsValid)
            {
                return services.Update(inventario);
            }
            return null;
        }
    }
}
