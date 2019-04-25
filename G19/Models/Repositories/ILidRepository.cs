using System.Collections.Generic;

namespace G19.Models.Repositories {
    public interface ILidRepository {
        IEnumerable<Lid> GetAll();
        Lid GetById(int id);
        void Add(Lid lid);
        void Remove(Lid lid);
        void SaveChanges();
        void RegisteerAanwezigheid(Lid lid);
        IEnumerable<Lid> GetByGraad(string graad);
    }
}
