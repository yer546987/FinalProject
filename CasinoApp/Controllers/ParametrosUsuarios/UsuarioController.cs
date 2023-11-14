using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.ParametrosUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private UsuariosServices services;
        public UsuarioController()
        {
            services = new UsuariosServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<UsuarioDto>> GetAll()
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
        public RequestResult<UsuarioDto> GetById([FromRoute] int id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<UsuarioDto> Create([FromBody] UsuarioDto usuario)
        {
            if (ModelState.IsValid)
            {
                return services.Create(usuario);
            }
            return RequestResult<UsuarioDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public UsuarioDto Update([FromBody] UsuarioDto usuario)
        {
            if (ModelState.IsValid)
            {
                return services.Update(usuario);
            }
            return null;
        }
    }
}
