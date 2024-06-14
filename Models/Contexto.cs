using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ProyectoTFG_League.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<AspectoModelo> Aspectos { get; set; }
        public DbSet<CampeonModelo> Campeones { get; set; }
        public DbSet<HabilidadModelo> Habilidades { get; set; }
        public DbSet<HechizoModelo> Hechizos { get; set; }
        public DbSet<ModoJuegoModelo> ModosJuegos { get; set; }
        public DbSet<ObjetoModelo> Objetos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjetoModelo>()
                .Ignore(o => o.Modo)
                .Property(o => o.Modos);
        }
        public DbSet<RolModelo> Roles { get; set; }


    }
}
