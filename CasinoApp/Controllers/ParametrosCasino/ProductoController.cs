using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosCasino
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private ProductoServices services;
        public ProductoController()
        {
            services = new ProductoServices();
        }


        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<ProductoDto>> GetAll()
        {
            return services.GetAll();
        }


        /// <summary>
        /// Este metodo retorna una especie por id
        /// </summary>
        /// <param name="id">Identificador de la especie</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id}")]
        public RequestResult<ProductoDto> GetById([FromRoute] int Id)
        {
            return services.GetById(Id);
        }

        [HttpPost]
        public RequestResult<ProductoDto> Create([FromBody] ProductoDto Producto)
        {
            if (ModelState.IsValid)
            {
                return services.Create(Producto);
            }
            return RequestResult<ProductoDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPost]
        [Route("Update")]
        public RequestResult<ProductoDto> Update([FromBody] ProductoDto producto)
        {
            if (ModelState.IsValid)
            {
                return services.Update(producto);
            }
            return null;
        }
    }
}
