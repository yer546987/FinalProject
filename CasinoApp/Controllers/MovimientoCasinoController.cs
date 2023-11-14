using CasinoApp.Application.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.MovimientoCasino;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoCasinoController : Controller
    {
        private MovimientoCasinoServices _services;

        public MovimientoCasinoController() 
        {
            _services = new MovimientoCasinoServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<MovimientoCasinoDto>> GetAll() 
        {
            return _services.GetAll();
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
            return _services.GetById(id);
        }

        [HttpPost]
        public RequestResult<MovimientoCasinoDto> Create([FromBody] MovimientoCasinoDto movimientoCasino)
        {
            if (ModelState.IsValid)
            {
                return _services.Create(movimientoCasino);
            }
            return RequestResult<MovimientoCasinoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public MovimientoCasinoDto Update([FromBody] MovimientoCasinoDto movimientoCasino)
        {
            if (ModelState.IsValid)
            {
                return _services.Update(movimientoCasino);
            }
            return null;
        }
    }
}
