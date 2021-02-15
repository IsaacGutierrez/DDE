using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dde.dataaccess.Models
{
    public partial class DDEContext : DbContext
    {
        public DDEContext()
        {
        }

        public DDEContext(DbContextOptions<DDEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CapacitacionMultimedia> CapacitacionMultimedia { get; set; }
        public virtual DbSet<Choferes> Choferes { get; set; }
        public virtual DbSet<EncabezadoPedidos> EncabezadoPedidos { get; set; }
        public virtual DbSet<Multimedia> Multimedia { get; set; }
        public virtual DbSet<ProgramacionMultimedia> ProgramacionMultimedia { get; set; }
        public virtual DbSet<TipoOrdenes> TipoOrdenes { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Vehiculos> Vehiculos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=USCLDCOMVMQ01;Database=DDE;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CapacitacionMultimedia>(entity =>
            {
                entity.ToTable("CapacitacionMultimedia", "Multimedia");

                entity.Property(e => e.CapacitacionMultimediaId).ValueGeneratedNever();

                entity.Property(e => e.FechaHoraCreacion).HasColumnType("datetime");

                entity.HasOne(d => d.EncabezadoPedido)
                    .WithMany(p => p.CapacitacionMultimedia)
                    .HasForeignKey(d => d.EncabezadoPedidoId)
                    .HasConstraintName("FK_CapacitacionMultimedia_EncabezadoPedidos");

                entity.HasOne(d => d.ProgramacionMultimedia)
                    .WithMany(p => p.CapacitacionMultimedia)
                    .HasForeignKey(d => d.ProgramacionMultimediaId)
                    .HasConstraintName("FK_CapacitacionMultimedia_ProgramacionMultimedia");
            });

            modelBuilder.Entity<Choferes>(entity =>
            {
                entity.HasKey(e => e.ChoferId);

                entity.ToTable("Choferes", "Ordenes");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumentoIdentidad)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EncabezadoPedidos>(entity =>
            {
                entity.HasKey(e => e.EncabezadoPedidoId);

                entity.ToTable("EncabezadoPedidos", "Ordenes");

                entity.Property(e => e.FechaPedido).HasColumnType("datetime");

                entity.Property(e => e.NombreTransporte)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPedido)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroValera)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Chofer)
                    .WithMany(p => p.EncabezadoPedidos)
                    .HasForeignKey(d => d.ChoferId)
                    .HasConstraintName("FK_EncabezadoPedidos_Choferes");

                entity.HasOne(d => d.TipoOrden)
                    .WithMany(p => p.EncabezadoPedidos)
                    .HasForeignKey(d => d.TipoOrdenId)
                    .HasConstraintName("FK_EncabezadoPedidos_TipoOrdenes");

                entity.HasOne(d => d.Vehiculo)
                    .WithMany(p => p.EncabezadoPedidos)
                    .HasForeignKey(d => d.VehiculoId)
                    .HasConstraintName("FK_EncabezadoPedidos_Vehiculos");
            });

            modelBuilder.Entity<Multimedia>(entity =>
            {
                entity.ToTable("Multimedia", "Multimedia");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgramacionMultimedia>(entity =>
            {
                entity.ToTable("ProgramacionMultimedia", "Multimedia");

                entity.Property(e => e.FechaHoraCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaHoraModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioProgramacion).HasColumnType("datetime");

                entity.Property(e => e.FechaTerminoProgramacion).HasColumnType("datetime");

                entity.HasOne(d => d.Multimedia)
                    .WithMany(p => p.ProgramacionMultimedia)
                    .HasForeignKey(d => d.MultimediaId)
                    .HasConstraintName("FK_ProgramacionMultimedia_Multimedia");

                entity.HasOne(d => d.UsuarioCreacion)
                    .WithMany(p => p.ProgramacionMultimedia)
                    .HasForeignKey(d => d.UsuarioCreacionId)
                    .HasConstraintName("FK_ProgramacionMultimedia_Usuarios");
            });

            modelBuilder.Entity<TipoOrdenes>(entity =>
            {
                entity.HasKey(e => e.TipoOrdenId);

                entity.ToTable("TipoOrdenes", "Ordenes");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.ToTable("Usuarios", "Configuracion");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehiculos>(entity =>
            {
                entity.HasKey(e => e.VehiculoId);

                entity.ToTable("Vehiculos", "Ordenes");

                entity.Property(e => e.Placa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlacaRastra)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
