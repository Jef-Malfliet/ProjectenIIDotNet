using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models.Repositories {
    public interface ISessionRepository {

        IEnumerable<Session> GetAll();
        Session GetById(int id);
        void Add(Session session);
        void Remove(Session session);
        void SaveChanges();
    }
}
