using CasinoApp.Aplication.Contracts;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Aplication.Services
{
    public class UsuariosServices : IUsuarioServices
    {
        public RequestResult<UsuarioDto> Create(UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int isUsuario)
        {
            throw new NotImplementedException();
        }

        public RequestResult<List<UsuarioDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public RequestResult<UsuarioDto> GetById(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public UsuarioDto Update(UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }
    }
}
