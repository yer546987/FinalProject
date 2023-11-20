using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientesController : Controller
    {
        private IngredientesServices services;
        public IngredientesController()
        {
            services = new IngredientesServices();
        }


        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<IngredientesDto>> GetAll()
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
        public RequestResult<IngredientesDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<IngredientesDto> Create([FromBody] IngredientesDto ingredientes)
        {
            if (ModelState.IsValid)
            {
                return services.Create(ingredientes);
            }
            return RequestResult<IngredientesDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPost]
        [Route("Update")]
        public RequestResult<IngredientesDto> Update([FromBody] IngredientesDto ingredientes)
        {
            if (ModelState.IsValid)
            {
                return services.Update(ingredientes);
            }
            return null;
        }
    }
}
