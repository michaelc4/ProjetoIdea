using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ProblemaMap : IEntityTypeConfiguration<ProblemaEntity>
    {
        public void Configure(EntityTypeBuilder<ProblemaEntity> builder)
        {
            builder.ToTable("Problema");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DesProblema).HasColumnType("LONGTEXT");
            builder.Property(p => p.IndTipoBeneficio).HasMaxLength(1);
            builder.Property(p => p.IndTipoSolucao).HasMaxLength(1);
            builder.Property(p => p.NumProbabilidadeInvestir);
            builder.Property(p => p.IndAtivo).HasMaxLength(1);
            builder.Property(p => p.IndAprovado).HasMaxLength(1);
            builder.Property(p => p.UsuarioId);
        }
    }
}
