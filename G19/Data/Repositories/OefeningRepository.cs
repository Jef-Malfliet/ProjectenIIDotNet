using G19.Models;
using G19.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Data.Repositories {
    public class OefeningRepository : IOefeningRepository {
        private readonly ApplicationDbContext _context;

        public OefeningRepository(ApplicationDbContext context) {
            _context = context;
        }
        public void Add(Oefening oefening) {
            _context.Oefeningen.Add(oefening);
        }

        public IEnumerable<Oefening> GetAll() {
            return _context.Oefeningen.ToList();
        }

        public Oefening GetById(int id) {
            return _context.Oefeningen.FirstOrDefault(l => l.Id == id);
        }

        public void Remove(Oefening oefening) {
            _context.Oefeningen.Remove(oefening);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
