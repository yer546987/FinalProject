using CasinoApp.Entities.Http;
using CasinoApp.Entities.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp.Application.Contracts
{
    public interface IUsuariosSercices
    {
        public UsuarioDto Update(UsuarioDto usuario);
        public bool Delete(int idUsuario);
        public RequestResult<List<UsuarioDto>> GetAll();
        public RequestResult<UsuarioDto> GetById(int idUsuario);
        RequestResult<UsuarioDto> Create(UsuarioDto usuario);
    }
}
