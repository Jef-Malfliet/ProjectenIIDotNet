using G19.Models;
using G19.Models.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace G19.Data.Repositories {
    public class OefeningRepository : IOefeningRepository {
        private readonly ApplicationDbContext _context;

        public OefeningRepository(ApplicationDbContext context) {
            _context = context;
        }
        public void Add(Oefening oefening) {
            _context.Oefeningen.Add(oefening);
        }

        public void AddComment(int id, string commentaar) {
            Oefening oef = GetById(id);
            Oefening_Comments comment = new Oefening_Comments(){OefeningId= oef.Id,Comments = commentaar};
            oef.Comments.Add(comment);
        }

        public IEnumerable<Oefening> GetAll() {
            return _context.Oefeningen.ToList();
        }


        public Oefening GetById(int id) {
            return _context.Oefeningen.Include(oef => oef.Comments).Include(oef => oef.Images).FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Oefening> GetOefeningenPerGraad(GraadEnum graad) {
            if (graad == GraadEnum.ALLES)
                return _context.Oefeningen;
            return _context.Oefeningen.Where(oef => oef.Graad.Equals(graad));
        }

        public void Remove(Oefening oefening) {
            _context.Oefeningen.Remove(oefening);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
