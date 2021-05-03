using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<IdeiaEntity> Ideias { get; set; }
        public DbSet<IdeiaAnexoEntity> IdeiaAnexos { get; set; }
        public DbSet<LikeEntity> Likes { get; set; }
        public DbSet<ProblemaEntity> Problemas { get; set; }
        public DbSet<ProblemaAnexoEntity> ProblemaAnexos { get; set; }
        public DbSet<VoluntarioEntity> Voluntarios { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UsuarioEntity>(new UsuarioMap().Configure);
            modelBuilder.Entity<IdeiaEntity>(new IdeiaMap().Configure);
            modelBuilder.Entity<IdeiaAnexoEntity>(new IdeiaAnexoMap().Configure);
            modelBuilder.Entity<LikeEntity>(new LikeMap().Configure);
            modelBuilder.Entity<ProblemaEntity>(new ProblemaMap().Configure);
            modelBuilder.Entity<ProblemaAnexoEntity>(new ProblemaAnexoMap().Configure);
            modelBuilder.Entity<VoluntarioEntity>(new VoluntarioMap().Configure);
        }
    }
}
