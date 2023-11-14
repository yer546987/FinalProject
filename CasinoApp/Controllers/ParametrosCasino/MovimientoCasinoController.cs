using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoCasinoController : Controller
    {
        private MovimientoCasinoServices services;

        public MovimientoCasinoController()
        {
            services = new MovimientoCasinoServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<MovimientoCasinoDto>> GetAll()
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
        public RequestResult<MovimientoCasinoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<MovimientoCasinoDto> Create([FromBody] MovimientoCasinoDto movimientoCasino)
        {
            if (ModelState.IsValid)
            {
                return services.Create(movimientoCasino);
            }
            return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public MovimientoCasinoDto Update([FromBody] MovimientoCasinoDto movimientoCasino)
        {
            if (ModelState.IsValid)
            {
                return services.Update(movimientoCasino);
            }
            return null;
        }
    }
}
