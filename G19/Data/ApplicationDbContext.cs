using System;
using System.Collections.Generic;
using System.Text;
using G19.Data.Mappers;
using G19.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace G19.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public DbSet<Lid> Leden { get; set; }
        public DbSet<Oefening> Oefeningen { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new LidConfiguration());
            builder.ApplyConfiguration(new OefeningConfiguration());
            builder.ApplyConfiguration(new Oefening_CommentsConfiguration());
            builder.ApplyConfiguration(new Oefening_ImagesConfiguration());
            builder.ApplyConfiguration(new Lid_AanwezighedenConfiguration());


        }

    }
}
