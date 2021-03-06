﻿using G19.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G19.Data.Mappers {
    public class Oefening_ImagesConfiguration : IEntityTypeConfiguration<Oefening_Images> {
        public void Configure(EntityTypeBuilder<Oefening_Images> builder) {
            builder.ToTable("Oefening_IMAGES");
            

            builder.Property(oi => oi.OefeningId).HasColumnName("Oefening_ID");
            builder.HasKey(table => new { table.OefeningId, table.Images });
        }
    }
}
