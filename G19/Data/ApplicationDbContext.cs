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


        }

        }
}
