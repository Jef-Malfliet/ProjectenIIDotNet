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
            return _context.Leden.Include(l => l.Aanwezigheden).OrderBy(l => l.Familienaam).ThenBy(l => l.Voornaam).ToList();
        }
        public IEnumerable<Lid> GetLedenInFormuleOfDay(DayOfWeek dag) {
            
            if(dag == DayOfWeek.Tuesday)
                return GetAll()
                    .Where(l => l.Lessen.Equals(FormuleEnum.Dinsdag) || l.Lessen.Equals(FormuleEnum.Dinsdag_Donderdag) 
                    || l.Lessen.Equals(FormuleEnum.Dinsdag_Zaterdag))
                    .ToList();
            if (dag == DayOfWeek.Wednesday)
                return GetAll()
                    .Where(l => l.Lessen.Equals(FormuleEnum.Woensdag) || l.Lessen.Equals(FormuleEnum.Woensdag_Zaterdag))
                    .ToList();
            if (dag == DayOfWeek.Thursday)
                return GetAll()
                    .Where(l => l.Lessen.Equals(FormuleEnum.Dinsdag_Donderdag))
                   .ToList();
            if (dag == DayOfWeek.Saturday)
                return _context.Leden.Include(l => l.Aanwezigheden)
                    .Where(l => l.Lessen.Equals(FormuleEnum.Dinsdag_Zaterdag) || l.Lessen.Equals(FormuleEnum.Woensdag_Zaterdag) || l.Lessen.Equals(FormuleEnum.Zaterdag))
                    .ToList();

            return new List<Lid>();
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
                return GetAll();
            } else if (graad.ToLower().Equals("zwart")) {
                return GetAll().Where(l => l.Graad.ToString("").ToLower().StartsWith("dan")).ToList();
            } else {
                return GetAll().Where(l => l.Graad.ToString("").ToLower() == graad.ToLower()).ToList();
            }

        }

        //public IEnumerable<Lid> GetByFormule(FormuleEnum formule) {
        //    return _context.Leden.Include(l => l.Aanwezigheden).Where(l => l.Lessen.Equals(formule)).OrderBy(l => l.Graad).ThenBy(l => l.Familienaam).ThenBy(l => l.Voornaam).ToList();
        //}

        public IEnumerable<Lid> GetByGraadEnFormuleOfDay(string graad, DayOfWeek dag) {
            if (graad.ToLower().Equals("alles")) {
                return GetLedenInFormuleOfDay(dag).ToList();
            } else if (graad.ToLower().Equals("zwart")) {
                return GetLedenInFormuleOfDay(dag).Where(l => l.Graad.ToString("").ToLower().StartsWith("dan")).ToList();
            } else {
                return GetLedenInFormuleOfDay(dag).Where(l => l.Graad.ToString("").ToLower() == graad.ToLower()).ToList();
            }

            
        }
    }
}
