using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoComida;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoComidaController : Controller
    {
        private TipoComidaServices services;

        public TipoComidaController()
        {
            services = new TipoComidaServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<TipoComidaDto>> GetAll()
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
        public RequestResult<TipoComidaDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<TipoComidaDto> Create([FromBody] TipoComidaDto tipoComida)
        {
            if (ModelState.IsValid)
            {
                return services.Create(tipoComida);
            }
            return RequestResult<TipoComidaDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPost]
        [Route("Update")]
        public RequestResult<TipoComidaDto> Update([FromBody] TipoComidaDto tipoComida)
        {
            if (ModelState.IsValid)
            {
                return services.Update(tipoComida);
            }
            return null;
        }
    }
}
