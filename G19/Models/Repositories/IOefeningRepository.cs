using System.Collections.Generic;

namespace G19.Models.Repositories {
    public interface IOefeningRepository {
        IEnumerable<Oefening> GetAll();
        Oefening GetById(int id);
        void Add(Oefening oefening);
        void Remove(Oefening oefening);
        void SaveChanges();
    }
}
