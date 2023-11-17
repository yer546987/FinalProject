using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Inventario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private InventarioServices services;

        public InventarioController()
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
        public RequestResult<InventarioDto> GetById([FromRoute] int id)
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
