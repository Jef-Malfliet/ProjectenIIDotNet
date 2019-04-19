using G19.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G19.Data.Mappers {
    public class Lid_AanwezighedenConfiguration : IEntityTypeConfiguration<Lid_Aanwezigheden> {

        public void Configure(EntityTypeBuilder<Lid_Aanwezigheden> builder) {
            builder.ToTable("Lid_AANWEZIGHEDEN");
            builder.HasKey(l => new { l.Aanwezigheid, l.LidId });
            builder.Property(l => l.LidId)
                    .HasColumnName("Lid_ID");

            builder.Property(l => l.Aanwezigheid)
                .HasColumnName("AANWEZIGHEDEN");
        }
    }
}
