using G19.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Data.Mappers {
    public class SessionConfiguration : IEntityTypeConfiguration<Session> {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Session> builder) {
            builder.ToTable("Session");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("ID");

            builder.Property(l => l.Date)
                .IsRequired(true);
            builder.Property(l => l.Formule)
                .IsRequired(true);
           
        }
        }
}
