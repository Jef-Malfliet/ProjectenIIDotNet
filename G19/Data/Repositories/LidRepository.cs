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
            return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l => l.Voornaam).ThenBy(l => l.Familienaam).ToList();
        }

        public IEnumerable<Lid> GetByGraad(string graad) {
            return _context.Leden.Where(l => (l.Graad.ToString().IndexOf("dan", StringComparison.OrdinalIgnoreCase) >= 0 ? "zwart" : l.Graad.ToString().ToLower()) == graad).ToList();
        }

        public Lid GetById(int id) {
            return _context.Leden.Include(l => l.Aanwezigheden).FirstOrDefault(l => l.Id == id);
        }

        public void RegisteerAanwezigheid(Lid lid) {
            var registreerLid = _context.Leden.FirstOrDefault(l => l.Id == lid.Id);
            if (lid.benIkAanwezigVandaag()) {
                var aanwezigheid = registreerLid.Aanwezigheden.FirstOrDefault(a => a.Aanwezigheid.Date == DateTime.Today);
                registreerLid.Aanwezigheden.Remove(aanwezigheid);

            } else {
                registreerLid.Aanwezigheden.Add(new Lid_Aanwezigheden {
                    Aanwezigheid = DateTime.Now,
                    LidId = registreerLid.Id
                });
            }
        }

        public void Remove(Lid lid) {
            _context.Leden.Remove(lid);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
