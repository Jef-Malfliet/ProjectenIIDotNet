using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models.Repositories {
    public interface ILidRepository {
        IEnumerable<Lid> GetAll();
        Lid GetById(int id);
        void Add(Lid lid);
        void Remove(Lid lid);
        void SaveChanges();
    }
}
