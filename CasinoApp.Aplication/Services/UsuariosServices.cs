using CasinoApp.Aplication.Contracts;
using CasinoApp.Aplication.DataAccess;
using CasinoApp.Aplication.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Usuario;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class UsuariosServices : IUsuarioServices
    {
        private CasinoAppContext _Context;

        public UsuariosServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<UsuarioDto> Create(UsuarioDto usuario)
        {
            try
            {
                if (usuario is null)
                    return RequestResult<UsuarioDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(usuario.Nombre))
                    return RequestResult<UsuarioDto>.CreateNoSuccess("El nombre del usuario es requerido");
                Usuario entity = new Usuario();
                entity.Nombre = usuario.Nombre;
                var result = _Context.Usuarios.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<UsuarioDto>.CreateNoSuccess("Ha ocurrido un error al registrar el usuario.");
                var resultado = new UsuarioDto()
                {
                    Id = result.Entity.Id,
                    Nombre = result.Entity.Nombre,
                    Usuario1 = result.Entity.Usuario1
                };
                return RequestResult<UsuarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<UsuarioDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int isUsuario)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<UsuarioDto>> GetAll()
        {
            try
            {
                var usuario = _Context.Usuarios.ToList();
                List<UsuarioDto> result = new List<UsuarioDto>();
                foreach (var item in usuario)
                {
                    result.Add(new UsuarioDto()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Usuario1 = item.Usuario1
                    });
                }
                return RequestResult<List<UsuarioDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<UsuarioDto> GetById(int idUsuario)
        {
            try
            {
                var usuario = _Context.Usuarios.Where(x => x.Id == idUsuario).FirstOrDefault();
                if (usuario is null) return RequestResult<UsuarioDto>.CreateNoSuccess($"No existe el usuario con identificador {idUsuario}");
                var resultado = new UsuarioDto()
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Usuario1 = usuario.Usuario1
                };
                return RequestResult<UsuarioDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<UsuarioDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public UsuarioDto Update(UsuarioDto usuario)
        {
            if (usuario is null) return null;
            if (usuario.Id is 0) return null;
            if (string.IsNullOrEmpty(usuario.Nombre)) return null;
            var entidad = _Context.Usuarios
                                .Where(x => x.Id.Equals(usuario.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Nombre = usuario.Nombre;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new UsuarioDto()
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Usuario1 = entidad.Usuario1
            };
        }
    }
}
