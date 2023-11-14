using CasinoApp.Entities.Http;
using CasinoApp.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IRolesServices
    {
        public RolesDto Update(RolesDto roles);
        public bool Delete(int idRoles);
        public RequestResult<List<RolesDto>> GetAll();
        public RequestResult<RolesDto> GetById(Guid idRoles);
        RequestResult<RolesDto> Create(RolesDto roles);
    }
}
