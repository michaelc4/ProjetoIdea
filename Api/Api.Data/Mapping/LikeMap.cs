using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class LikeMap : IEntityTypeConfiguration<LikeEntity>
    {
        public void Configure(EntityTypeBuilder<LikeEntity> builder)
        {
            builder.ToTable("Like");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IpUsr).HasMaxLength(50);
            builder.Property(p => p.ProblemaId);
            builder.Property(p => p.IdeiaId);
        }
    }
}
