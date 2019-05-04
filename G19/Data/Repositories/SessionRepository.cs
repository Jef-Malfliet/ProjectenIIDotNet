//using G19.Models;
//using G19.Models.Repositories;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace G19.Data.Repositories {
//    public class SessionRepository : ISessionRepository {
//        private readonly ApplicationDbContext _context;

//        public SessionRepository(ApplicationDbContext context) {
//            _context = context;
//        }
//        public void Add(Session session) {
//            _context.Sessions.Add(session);
//        }

//        public IEnumerable<Session> GetAll() {
//            return _context.Sessions;
//        }

//        public Session GetById(int id) {
//            return _context.Sessions.FirstOrDefault(s => s.Id == id);
//        }

//        public void Remove(Session session) {
//             _context.Sessions.Remove(session);

//        }

//        public void SaveChanges() {
//            _context.SaveChanges();
//        }
//    }
//}
