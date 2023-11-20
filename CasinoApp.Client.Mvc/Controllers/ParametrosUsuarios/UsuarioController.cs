using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using CasinoApp.Entities.Usuario;
using CasinoApp.Client.Helper;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.Login;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CasinoApp.Client.Mvc.Controllers.ParametrosUsuarios
{
    public class UsuarioController : Controller
    {
        private readonly CasinoAppClientMvcContext _context;

        public UsuarioController(CasinoAppClientMvcContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            MVAHttpClient client = new MVAHttpClient();
            var resultado = client.Post<RequestResult<UsuarioDto>>("/api/Usuario", login);

            if (resultado.IsSuccessful)
            {
                var nombreUsuario = resultado.Result.Nombre;
                var rol = resultado.Result.Rol;

                if (rol == 1)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, nombreUsuario),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, nombreUsuario),
                        new Claim(ClaimTypes.Role, "Usuario"),
                    };
                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["MensajeError"] = "Usuario o contraseña incorrecta";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
