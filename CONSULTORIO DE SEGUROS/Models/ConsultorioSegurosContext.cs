using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CONSULTORIO_DE_SEGUROS.Models;

public partial class ConsultorioSegurosContext : DbContext
{
    public ConsultorioSegurosContext()
    {
    }

    public ConsultorioSegurosContext(DbContextOptions<ConsultorioSegurosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asegurados> Asegurados { get; set; }

    public virtual DbSet<AseguradosSeguros> AseguradosSeguros { get; set; }

    public virtual DbSet<Seguros> Seguros { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //     => optionsBuilder.UseSqlServer("Server=(local);Database=CONSULTORIO_SEGUROS; User Id=sa ;Password=Alejorb2001; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asegurados>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asegurad__3214EC0776CBF3EB");

            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AseguradosSeguros>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asegurad__3214EC072E45A10E");

            entity.HasOne(d => d.Asegurado).WithMany(p => p.AseguradosSeguros)
                .HasForeignKey(d => d.AseguradoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asegurado__Asegu__4D94879B");

            entity.HasOne(d => d.Seguro).WithMany(p => p.AseguradosSeguros)
                .HasForeignKey(d => d.SeguroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Asegurado__Segur__4E88ABD4");
        });

        modelBuilder.Entity<Seguros>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Seguros__3214EC074E42B2D2");

            entity.Property(e => e.CodigoSeguro)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NombreSeguro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Prima).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SumaAsegurada).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

