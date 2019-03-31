using System;
using System.Collections.Generic;
using System.Text;
using G19.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace G19.Data {
    public class ApplicationDbContext : IdentityDbContext {

        public DbSet<Lid> Leden { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
    }
}
