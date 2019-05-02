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
            return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l => l.Graad).ThenBy(l => l.Familienaam).ThenBy(l => l.Voornaam).ToList();
        }

        public Lid GetById(int id) {
            return _context.Leden.Include(l => l.Aanwezigheden).FirstOrDefault(l => l.Id == id);
        }

        public Lid GetByNames(string voornaam,string familienaam) {
            return _context.Leden.Include(l => l.Aanwezigheden).FirstOrDefault(l => l.Voornaam.Equals(voornaam, StringComparison.OrdinalIgnoreCase) && l.Familienaam.Equals(familienaam, StringComparison.OrdinalIgnoreCase));
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

        public IEnumerable<Lid> GetByGraad(string graad) {
            if (graad.ToLower().Equals("alles")) {
                return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l=>l.Familienaam).ThenBy(l=>l.Voornaam).ToList();
            } else if (graad.ToLower().Equals("zwart")) {
                return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l => l.Familienaam).ThenBy(l => l.Voornaam).Where(l => l.Graad.ToString("").ToLower().StartsWith("dan")).ToList();
            } else {
                return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l => l.Familienaam).ThenBy(l => l.Voornaam).Where(l => l.Graad.ToString("").ToLower() == graad.ToLower()).ToList();
            }

        }

        public IEnumerable<Lid> GetByFormule(FormuleEnum formule) {
            return _context.Leden.Include(l => l.Aanwezigheden).Where(l => l.Lessen.Equals(formule)).OrderBy(l => l.Graad).ThenBy(l => l.Familienaam).ThenBy(l => l.Voornaam).ToList();
        }

        public IEnumerable<Lid> GetByGraadEnFormule(string graad, FormuleEnum formule) {
          return GetByGraad(graad).Where(l => l.Lessen.Equals(formule)).OrderBy(l => l.Graad).ThenBy(l => l.Familienaam).ThenBy(l => l.Voornaam).ToList();
        }
    }
}
