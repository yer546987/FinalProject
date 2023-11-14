using CasinoApp.Application.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CasinoApp.Api.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesContollers : Controller
    {
        private RolesServices services;

        public RolesContollers()
        {
            services = new RolesServices();
        }

        /// <summary>
        /// Este metodo retorna todas las especies registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RequestResult<List<RolesDto>> GetAll()
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
        public RequestResult<RolesDto> GetById([FromRoute] Guid id)
        {
            return services.GetById(id);
        }

        [HttpPost]
        public RequestResult<RolesDto> Create([FromBody] RolesDto roles)
        {
            if (ModelState.IsValid)
            {
                return services.Create(roles);
            }
            return RequestResult<RolesDto>.CreateNoSuccess("El modelo no es valido");
        }


        [HttpPut]
        public RolesDto Update([FromBody] RolesDto roles)
        {
            if (ModelState.IsValid)
            {
                return services.Update(roles);
            }
            return null;
        }
    }
}
