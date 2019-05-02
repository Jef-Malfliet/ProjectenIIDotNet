using System;
using System.Collections.Generic;

namespace G19.Models.Repositories {
    public interface ILidRepository {
        IEnumerable<Lid> GetAll();
        Lid GetById(int id);
        Lid GetByNames(string voornaam, string familienaam);
       
        void Add(Lid lid);
        void Remove(Lid lid);
        void SaveChanges();
        void RegisteerAanwezigheid(Lid lid);
        IEnumerable<Lid> GetByGraad(string graad);
        IEnumerable<Lid> GetByGraadEnFormule(string graad, FormuleEnum formule);
        IEnumerable<Lid> GetLedenInFormuleOfDay(DayOfWeek dag);
        IEnumerable<Lid> GetByFormule(FormuleEnum formule);
    }
}
