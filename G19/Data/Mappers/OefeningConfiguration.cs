using G19.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G19.Data.Mappers {
    public class OefeningConfiguration : IEntityTypeConfiguration<Oefening> {
        public void Configure(EntityTypeBuilder<Oefening> builder) {
            builder.ToTable("OEFENING");

            builder.HasKey(o=> o.Id);

            builder.Property(o => o.AantalKeerBekeken)
              .IsRequired(false);

            builder.Property(o => o.Graad)
              .IsRequired(true);
              
            builder.Property(o => o.Naam)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(o => o.Uitleg)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(o => o.Video)
                .IsRequired(false)
                .HasMaxLength(255);

        }
    }
}
