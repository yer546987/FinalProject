using CasinoApp.Aplication.Services;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostoCasinoController : Controller
    {
        private CostoCasinoServices services;
        public CostoCasinoController()
        {
            services = new CostoCasinoServices();
        }


        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<CostoCasinoDto>> GetAll()
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
        public RequestResult<CostoCasinoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<CostoCasinoDto> Create([FromBody] CostoCasinoDto costoCasino)
        {
            if (ModelState.IsValid)
            {
                return services.Create(costoCasino);
            }
            return RequestResult<CostoCasinoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public CostoCasinoDto Update([FromBody] CostoCasinoDto costoCasino)
        {
            if (ModelState.IsValid)
            {
                return services.Update(costoCasino);
            }
            return null;
        }
    }
}
