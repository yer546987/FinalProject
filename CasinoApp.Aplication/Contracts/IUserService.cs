using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using CasinoApp.Entities.Http;
using CasinoApp.Entities.Usuario;
using Microsoft.EntityFrameworkCore;

namespace CasinoApp.Aplication.Contracts
{
    public interface IUserService
    {
        RequestResult<UsuarioDto> GetUsers(string Usuario, string Pass);
    }
}
