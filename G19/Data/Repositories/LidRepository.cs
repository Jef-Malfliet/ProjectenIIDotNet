using G19.Models;
using G19.Models.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Data.Repositories {
    public class LidRepository : ILidRepository {
        private readonly ApplicationDbContext _context;

        public LidRepository(ApplicationDbContext context) {
            _context = context;
        }

        public void Add(Lid lid) {
            _context.Leden.Add(lid);
        }

        public IEnumerable<Lid> GetAll() {
            return _context.Leden.Include(l=>l.Aanwezigheden).OrderBy(l => l.Voornaam).ThenBy(l => l.Familienaam).ToList();
        }

        public Lid GetById(int id) {
            return _context.Leden.Include(l => l.Aanwezigheden).FirstOrDefault(l => l.Id == id);
        }

        public void Remove(Lid lid) {
            _context.Leden.Remove(lid);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
