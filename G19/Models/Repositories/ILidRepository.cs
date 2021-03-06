﻿using System;
using System.Collections.Generic;

namespace G19.Models.Repositories {
    public interface ILidRepository {
        IEnumerable<Lid> GetAll();
        Lid GetById(int id);
        Lid GetByEmail(string email);
        Lid GetByNames(string voornaam, string familienaam);
       
        void Add(Lid lid);
        void Remove(Lid lid);
        void SaveChanges();
        void RegisteerAanwezigheid(Lid lid);

        IEnumerable<Lid> GetByGraad(string graad);
        IEnumerable<Lid> GetByGraadEnFormuleOfDay(string graad, DayOfWeek dag);
        IEnumerable<Lid> GetLedenInFormuleOfDay(DayOfWeek dag);
     
    }
}
