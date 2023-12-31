﻿using System;
using System.Collections.Generic;
using CasinoApp.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace CasinoApp.Application.DataAccess;

public partial class CasinoAppContext : DbContext
{
    public CasinoAppContext()
    {
    }

    public CasinoAppContext(DbContextOptions<CasinoAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostoCasino> CostoCasinos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<GrupoEmpleado> GrupoEmpleados { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MovimientoCasino> MovimientoCasinos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TipoComidum> TipoComida { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoEmpleado> TipoEmpleados { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=YEFERSON-BELTRA\\MSSQLSERVERR;Initial Catalog=CasinoApp;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostoCasino>(entity =>
        {
            entity.ToTable("CostoCasino");

            entity.HasOne(d => d.IdGrupoEmpleadoNavigation).WithMany(p => p.CostoCasinos)
                .HasForeignKey(d => d.IdGrupoEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CostoCasino_GrupoEmpleado");

            entity.HasOne(d => d.IdTipoComidaNavigation).WithMany(p => p.CostoCasinos)
                .HasForeignKey(d => d.IdTipoComida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CostoCasino_TipoComida");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.ToTable("Empleado");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Identificacion).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.IdGrupoENavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdGrupoE)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_GrupoEmpleado");

            entity.HasOne(d => d.IdTipoEmpleadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTipoEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_TipoEmpleado");

            entity.HasOne(d => d.IdTipoIdentificacionNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdTipoIdentificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Empleado_TipoDocumentos");
        });

        modelBuilder.Entity<GrupoEmpleado>(entity =>
        {
            entity.ToTable("GrupoEmpleado");

            entity.Property(e => e.NombreGrupo)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_InventarioMecatos");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cantidad)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.IdInventarioNavigation).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.IdInventario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_UnidadMedida");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.ToTable("Inventario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.Mecatos)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.Producto)
                .IsRequired()
                .HasMaxLength(255)
                .IsFixedLength();

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_UnidadMedida1");
        });

        modelBuilder.Entity<MovimientoCasino>(entity =>
        {
            entity.ToTable("MovimientoCasino");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.HoraRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.MovimientoCasinos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoCasino_Empleado");

            entity.HasOne(d => d.IdGrupoEmpleadoNavigation).WithMany(p => p.MovimientoCasinos)
                .HasForeignKey(d => d.IdGrupoEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoCasino_GrupoEmpleado");

            entity.HasOne(d => d.IdTipoComidaNavigation).WithMany(p => p.MovimientoCasinos)
                .HasForeignKey(d => d.IdTipoComida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoCasino_TipoComida");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Rol)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<TipoComidum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.TiempoFinal).HasColumnType("datetime");
            entity.Property(e => e.TiempoInicial).HasColumnType("datetime");

            entity.HasOne(d => d.IdIngredientesNavigation).WithMany(p => p.TipoComida)
                .HasForeignKey(d => d.IdIngredientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoComida_Ingredientes");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.TipoIdentificacion)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<TipoEmpleado>(entity =>
        {
            entity.ToTable("TipoEmpleado");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Contraseña)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Usuario1)
                .IsRequired()
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
