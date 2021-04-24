using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class IdeiaMap : IEntityTypeConfiguration<IdeiaEntity>
    {
        public void Configure(EntityTypeBuilder<IdeiaEntity> builder)
        {
            builder.ToTable("Ideia");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DesIdeia).HasColumnType("LONGTEXT");
            builder.Property(p => p.DesMotivoInvestir).HasColumnType("LONGTEXT");
            builder.Property(p => p.IndInteresseCompartilhar).HasMaxLength(1);
            builder.Property(p => p.IndNivelDesenvolvimento).HasMaxLength(1);
            builder.Property(p => p.IndNivelSigilo).HasMaxLength(1);
            builder.Property(p => p.DesComentario).HasColumnType("LONGTEXT");
            builder.Property(p => p.NumPotencialDisrupcao);
            builder.Property(p => p.NumPessoasImpactadas);
            builder.Property(p => p.NumPotencialGanho);
            builder.Property(p => p.NumValorInvestimento);
            builder.Property(p => p.NumImpactoAmbiental);
            builder.Property(p => p.NumPontuacaoGeral);
            builder.Property(p => p.IndAtivo).HasMaxLength(1);
            builder.Property(p => p.IndAprovado).HasMaxLength(1);
            builder.Property(p => p.UsuarioId);
        }
    }
}
