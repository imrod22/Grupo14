namespace HomeSwitchHome
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HomeSwitchHomeDB : DbContext
    {
        public HomeSwitchHomeDB()
            : base("name=HomeSwitchHomeDB")
        {
        }

        public virtual DbSet<ADMINISTRADOR> ADMINISTRADOR { get; set; }
        public virtual DbSet<CLIENTE> CLIENTE { get; set; }
        public virtual DbSet<HOTSALE> HOTSALE { get; set; }
        public virtual DbSet<IMAGEN> IMAGEN { get; set; }
        public virtual DbSet<NOVEDAD_PROPIEDAD> NOVEDAD_PROPIEDAD { get; set; }
        public virtual DbSet<PREMIUM> PREMIUM { get; set; }
        public virtual DbSet<PROPIEDAD> PROPIEDAD { get; set; }
        public virtual DbSet<RESERVA> RESERVA { get; set; }
        public virtual DbSet<SUBASTA> SUBASTA { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
        public virtual DbSet<PUJA> PUJA { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIENTE>()
                .HasMany(e => e.NOVEDAD_PROPIEDAD)
                .WithRequired(e => e.CLIENTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CLIENTE>()
                .HasMany(e => e.RESERVA)
                .WithRequired(e => e.CLIENTE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HOTSALE>()
                .Property(e => e.Precio)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PROPIEDAD>()
                .HasMany(e => e.IMAGEN)
                .WithRequired(e => e.PROPIEDAD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROPIEDAD>()
                .HasMany(e => e.RESERVA)
                .WithRequired(e => e.PROPIEDAD)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SUBASTA>()
                .Property(e => e.ValorMinimo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<SUBASTA>()
                .Property(e => e.ValorActual)
                .HasPrecision(10, 2);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.ADMINISTRADOR)
                .WithRequired(e => e.USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USUARIO>()
                .HasMany(e => e.CLIENTE)
                .WithRequired(e => e.USUARIO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PUJA>()
               .Property(e => e.Monto)
                .HasPrecision(10, 2);
        }
    }
}
