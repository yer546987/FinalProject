using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.TipoDocumento;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.Funcionarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoControllers : Controller
    {
        private TipoDocumentoServices services;

        public TipoDocumentoControllers()
        {
            services = new TipoDocumentoServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<TipoDocumentoDto>> GetAll()
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
        public RequestResult<TipoDocumentoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<TipoDocumentoDto> Create([FromBody] TipoDocumentoDto tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                return services.Create(tipoDocumento);
            }
            return RequestResult<TipoDocumentoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public TipoDocumentoDto Update([FromBody] TipoDocumentoDto tipoDocumento)
        {
            if (ModelState.IsValid)
            {
                return services.Update(tipoDocumento);
            }
            return null;
        }
    }
}
