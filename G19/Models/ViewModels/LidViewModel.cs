using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [StringLength(2, ErrorMessage = "Maximum 2")]

        public string Rijksregisternummer1 { get; set; }
        [StringLength(2)]

        public string Rijksregisternummer2 { get; set; }
        [StringLength(2)]

        public string Rijksregisternummer3 { get; set; }
        [StringLength(3)]

        public string Rijksregisternummer4 { get; set; }
        [StringLength(2)]

        public string Rijksregisternummer5 { get; set; }

        public string Busnummer { get; set; }
        public string Huisnummer { get; set; }
        public string EmailOuders { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Geslacht { get; set; }
        [EnumDataType(typeof(GraadEnum))]
        public GraadEnum Graad { get; set; }
        [EnumDataType(typeof(LandEnum))]
        public LandEnum Land { get; set; }
        [EnumDataType(typeof(FormuleEnum))]
        public FormuleEnum Lessen { get; set; }
        public string Postcode { get; set; }
        public string Stad { get; set; }
        public string StraatNaam { get; set; }
        [EnumDataType(typeof(RolTypeEnum))]
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
            Rijksregisternummer1 = lid.Rijksregisternummer.Substring(0,2);
            Rijksregisternummer2 = lid.Rijksregisternummer.Substring(3,2);
            Rijksregisternummer3 = lid.Rijksregisternummer.Substring(6, 2);
            Rijksregisternummer4 = lid.Rijksregisternummer.Substring(9, 3);
            Rijksregisternummer5 = lid.Rijksregisternummer.Substring(13, 2);

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
