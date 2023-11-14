using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Entities.Empleado;

namespace CasinoApp.Client.Mvc.Data
{
    public class CasinoAppClientMvcContext : DbContext
    {
        public CasinoAppClientMvcContext (DbContextOptions<CasinoAppClientMvcContext> options)
            : base(options)
        {
        }

        public DbSet<CasinoApp.Entities.Empleado.EmpleadoDto> EmpleadoDto { get; set; } = default!;
    }
}
