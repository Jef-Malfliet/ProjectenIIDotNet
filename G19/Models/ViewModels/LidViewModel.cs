using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace G19.Models.ViewModels {
    public class LidViewModel {

        //hier moeten ook nog restricties in !!
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Email { get; set; }
        public string GSM { get; set; }
        public string Telefoon { get; set; }
        public string Rijksregisternummer { get; set; }
        public string Busnummer { get; set; }
        public string Huisnummer { get; set; }
        public string EmailOuders { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Geslacht { get; set; }
        public GraadEnum Graad { get; set; }
        public LandEnum Land { get; set; }
        public FormuleEnum Lessen { get; set; }
        public string Postcode { get; set; }
        public string Stad { get; set; }
        public string StraatNaam { get; set; }
        public RolTypeEnum Roltype { get; set; }
        public string Wachtwoord { get; set; }
        public LidViewModel() {

        }
        public LidViewModel(Lid lid) {
            Id = lid.Id;
            Voornaam = lid.Voornaam;
            Achternaam = lid.Familienaam;
            Email = lid.Email;
            GSM = lid.GSM;
            Telefoon = lid.Telefoon;
            Rijksregisternummer = lid.Rijksregisternummer;
            Busnummer = lid.Busnummer;
            Huisnummer = lid.Huisnummer;
            EmailOuders = lid.EmailOuders;
            GeboorteDatum = lid.GeboorteDatum;
            Geslacht = lid.Geslacht;
            Graad = lid.Graad;
            Land = lid.Land;
            Lessen = lid.Lessen;
            Postcode = lid.PostCode;
            Stad = lid.Stad;
            StraatNaam = lid.StraatNaam;
            Roltype = lid.Roltype;
            Wachtwoord = lid.Wachtwoord;
        }
    }
}
