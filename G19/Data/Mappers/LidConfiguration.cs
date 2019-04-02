using G19.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G19.Data.Mappers {
    public class LidConfiguration : IEntityTypeConfiguration<Lid> {
        public void Configure(EntityTypeBuilder<Lid> builder) {
            builder.ToTable("LID");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Busnummer)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(l => l.Email)
                .IsRequired(true)
                .HasMaxLength(60);

            builder.Property(l => l.EmailOuders)
                .IsRequired(false)
                .HasMaxLength(60);

            builder.Property(l => l.Familienaam)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(l => l.GeboorteDatum)
                .IsRequired(true);

            builder.Property(l => l.Geslacht)
                .IsRequired(true)
                .HasMaxLength(25);

            builder.Property(l => l.GSM)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(l => l.Huisnummer)
                .IsRequired(true)
                .HasMaxLength(3);

            builder.Property(l => l.PostCode)
                .IsRequired(true)
                .HasMaxLength(4);

            builder.Property(l => l.Rijksregisternummer)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(l => l.Stad)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(l => l.StraatNaam)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(l => l.Telefoon)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(l => l.Voornaam)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(l => l.Wachtwoord)
                .IsRequired(true)
                .HasMaxLength(20);
        }
    }
}
