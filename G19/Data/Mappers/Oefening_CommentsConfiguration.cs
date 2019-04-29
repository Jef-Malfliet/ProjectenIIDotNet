using G19.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Data.Mappers {
    public class Oefening_CommentsConfiguration : IEntityTypeConfiguration<Oefening_Comments> {

        public void Configure(EntityTypeBuilder<Oefening_Comments> builder) {
            builder.ToTable("Oefening_COMMENTS");

            builder.Property(t => t.OefeningId).HasColumnName("Oefening_ID");
            builder.Property(t => t.TimeCreated).HasColumnName("TimeCreated");
            builder.HasKey(table => new { table.OefeningId, table.Comments });

        }
    }
}
