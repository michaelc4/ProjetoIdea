using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.DesEmail).IsUnique();
            builder.Property(p => p.DesNome).HasMaxLength(50);
            builder.Property(p => p.DesImagem).HasColumnType("LONGTEXT");
            builder.Property(p => p.DesEmail).HasMaxLength(100);
            builder.Property(p => p.DesSenha).HasMaxLength(300);
            builder.Property(p => p.DesTelefone).HasMaxLength(20);
            builder.Property(p => p.DesEspecialidade).HasMaxLength(300);
            builder.Property(p => p.DesExperiencia).HasMaxLength(3000);
        }
    }
}
