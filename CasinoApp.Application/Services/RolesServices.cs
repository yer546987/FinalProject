using CasinoApp.Application.Contracts;
using CasinoApp.Application.DataAccess;
using CasinoApp.Application.Models;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoApp.Application.Services
{
    public class RolesServices : IRolesServices
    {
        private CasinoAppContext _Context;

        public RolesServices() 
        {
            _Context = new CasinoAppContext();
        }
        public RequestResult<RolesDto> Create(RolesDto roles)
        {
            try
            {
                if (roles is null)
                    return RequestResult<RolesDto>.CreateNoSuccess("Los datos son requeridos");
                if (string.IsNullOrEmpty(roles.Rol))
                    return RequestResult<RolesDto>.CreateNoSuccess("El nombre del rol es requerido");
                Role entity = new Role();
                entity.Rol = roles.Rol;
                var result = _Context.Roles.Add(entity);
                int rows = _Context.SaveChanges();
                if (rows is 0)
                    return RequestResult<RolesDto>.CreateNoSuccess("Ha ocurrido un error al registrar el rol.");
                var resultado = new RolesDto()
                {
                    Rol = result.Entity.Rol,
                    Id = result.Entity.Id
                };
                return RequestResult<RolesDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<RolesDto>.CreateError(ex.Message);
            }
        }

        public bool Delete(int idRoles)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<RolesDto>> GetAll()
        {
            try
            {
                var roles = _Context.Roles.ToList();
                List<RolesDto> result = new List<RolesDto>();
                foreach (var item in roles)
                {
                    result.Add(new RolesDto()
                    {
                        Rol = item.Rol,
                        Id = item.Id
                    });
                }
                return RequestResult<List<RolesDto>>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RequestResult<RolesDto> GetById(Guid idRoles)
        {
            try
            {
                var roles = _Context.Roles.Where(x => x.Id == idRoles).FirstOrDefault();
                if (roles == null) return RequestResult<RolesDto>.CreateNoSuccess($"No existe el rol con identificador {idRoles}");
                var resultado = new RolesDto()
                {
                    Rol = roles.Rol,
                    Id = roles.Id
                };
                return RequestResult<RolesDto>.CreateSuccess(resultado);
            }
            catch (Exception ex)
            {
                return RequestResult<RolesDto>.CreateError($"Ha ocurrido un error: {ex.Message}");
            }
        }

        public RolesDto Update(RolesDto roles)
        {
            if (roles == null) return null;
            if (roles.Id.Equals(Guid.Empty)) return null;
            if (string.IsNullOrEmpty(roles.Rol)) return null;
            var entidad = _Context.Roles
                                .Where(x => x.Id.Equals(roles.Id))
                                .FirstOrDefault();
            if (entidad == null) return null;
            entidad.Rol = roles.Rol;
            _Context.Attach(entidad);
            _Context.Entry(entidad).State = EntityState.Modified;
            _Context.SaveChanges();
            return new RolesDto()
            {
                Rol = entidad.Rol,
                Id = entidad.Id
            };
        }
    }
}
