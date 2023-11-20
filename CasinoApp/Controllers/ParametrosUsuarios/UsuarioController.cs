using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.Services;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Login;
using CasinoApp.Entities.Usuario;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasinoApp.Api.Controllers.ParametrosUsuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private UserService services;
        public UsuarioController()
        {
            services = new UserService();
        }

        [HttpPost]
        public RequestResult<UsuarioDto> GetUsers(Login login)

        {
            return services.GetUsers(login.Usuario, login.Pass);          
        }

    }
}
