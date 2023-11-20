using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Producto;
using CasinoApp.Entities.Usuario;
using Microsoft.EntityFrameworkCore;


namespace CasinoApp.Aplication.Services
{
    public class UserService : IUserService
    {
        private CasinoAppContext _context;

        public UserService()
        {
            _context = new CasinoAppContext();
        }

        public RequestResult<UsuarioDto> GetUsers(string Usuario, string Pass)
        {
            UsuarioDto user_found =  _context.Usuarios
                .Where(u => u.Usuario1 == Usuario && u.Contraseña == Pass)
                .Select(u => new UsuarioDto
                {
                    // Mapear propiedades según las necesidades
                    Id = u.Id,
                    Usuario1 = u.Usuario1,
                    Nombre  =u.Nombre,
                    Pass = u.Contraseña,
                    Rol = u.IdRol                   
                })
                .FirstOrDefault();
            if (user_found == null)
            {
                return RequestResult<UsuarioDto>.CreateNoSuccess("Usuario o contraseña incorrecta");
            }
            return RequestResult<UsuarioDto>.CreateSuccess(user_found);
        }
       
    }

}
