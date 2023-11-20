using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Entities.Empleado;
using CasinoApp.Entities.GrupoEmpleado;
using CasinoApp.Entities.TipoDocumento;
using CasinoApp.Entities.TipoEmpleado;
using CasinoApp.Entities.MovimientoCasino;
using CasinoApp.Entities.Usuario;
using CasinoApp.Entities.TipoComida;
using CasinoApp.Entities.Inventario;
using CasinoApp.Entities.Ingredientes;
using CasinoApp.Entities.CostoCasino;
using CasinoApp.Entities.UnidadMedida;
using CasinoApp.Entities.Producto;

namespace CasinoApp.Client.Mvc.Data
{
    public class CasinoAppClientMvcContext : DbContext
    {
        public CasinoAppClientMvcContext (DbContextOptions<CasinoAppClientMvcContext> options)
            : base(options)
        {
        }
        public DbSet<CasinoApp.Entities.Empleado.EmpleadoDto> EmpleadoDto { get; set; } = default!;

        public DbSet<CasinoApp.Entities.GrupoEmpleado.GrupoEmpleadoDto> GrupoEmpleadoDto { get; set; }

        public DbSet<CasinoApp.Entities.TipoDocumento.TipoDocumentoDto> TipoDocumentoDto { get; set; }

        public DbSet<CasinoApp.Entities.TipoEmpleado.TipoEmpleadoDto> TipoEmpleadoDto { get; set; }

        public DbSet<CasinoApp.Entities.MovimientoCasino.MovimientoCasinoDto> MovimientoCasinoDto { get; set; }

        public DbSet<CasinoApp.Entities.Usuario.UsuarioDto> UsuarioDto { get; set; }

        public DbSet<CasinoApp.Entities.TipoComida.TipoComidaDto> tipoComida { get; set; }

        public DbSet<CasinoApp.Entities.Inventario.InventarioDto> InventarioDto { get; set; }

        public DbSet<CasinoApp.Entities.Ingredientes.IngredientesDto> IngredientesDto { get; set; }

        public DbSet<CasinoApp.Entities.CostoCasino.CostoCasinoDto> CostoCasinoDto { get; set; }

        public DbSet<CasinoApp.Entities.UnidadMedida.UnidadMedidaDto> UnidadMedidaDto { get; set; }

        public DbSet<CasinoApp.Entities.Producto.TipoProductoDto> TipoProductoDto { get; set; }

        public DbSet<CasinoApp.Entities.Producto.ProductoDto> ProductoDto { get; set; }
    }
}
