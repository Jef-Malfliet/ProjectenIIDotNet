//using G19.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace G19.Data.Mappers {
//    public class Session_LidConfiguration : IEntityTypeConfiguration<Session_Lid> {
//        public void Configure(EntityTypeBuilder<Session_Lid> builder) {
//            builder.ToTable("SESSION_LID");
//            builder.Property(l => l.LidId)
//                    .HasColumnName("Lid_ID");
//            builder.Property(l => l.SessionId)
//                    .HasColumnName("Session_ID");
//            builder.HasKey(l => new { l.SessionId, l.LidId });
            
//        }
//        }
//}
