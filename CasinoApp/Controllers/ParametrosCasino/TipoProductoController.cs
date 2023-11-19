using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.Producto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProductoController : Controller 
    {
            
        private TipoProductosServices services;
        public TipoProductoController()
        {
            services = new TipoProductosServices();
        }


        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<TipoProductoDto>> GetAll()
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
        public RequestResult<TipoProductoDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<TipoProductoDto> Create([FromBody] TipoProductoDto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                return services.Create(tipoProducto);
            }
            return RequestResult<TipoProductoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPost]
        [Route("Update")]
        public RequestResult<TipoProductoDto> Update([FromBody] TipoProductoDto tipoProducto)
        {
            if (ModelState.IsValid)
            {
                return services.Update(tipoProducto);
            }
            return null;
        }
    }
}
