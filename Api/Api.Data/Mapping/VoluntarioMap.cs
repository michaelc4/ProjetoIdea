using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class VoluntarioMap : IEntityTypeConfiguration<VoluntarioEntity>
    {
        public void Configure(EntityTypeBuilder<VoluntarioEntity> builder)
        {
            builder.ToTable("Voluntario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UsuarioId);
            builder.Property(p => p.ProblemaId);
            builder.Property(p => p.IdeiaId);
        }
    }
}
