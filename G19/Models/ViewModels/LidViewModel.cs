using System;
using System.ComponentModel.DataAnnotations;

namespace G19.Models.ViewModels {
    public class LidViewModel {

        #region fields
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string Voornaam { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string Achternaam { get; set; }

        [StringLength(60, ErrorMessage = "Maximum 60"), Required]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20"), Required]
        public string GSM { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20"), Required]
        public string Telefoon { get; set; }

        [StringLength(2, ErrorMessage = "Maximum 2"), Required]
        public string Rijksregisternummer1 { get; set; }

        [StringLength(2, ErrorMessage = "Maximum 2"), Required]
        public string Rijksregisternummer2 { get; set; }

        [StringLength(2, ErrorMessage = "Maximum 2"), Required]
        public string Rijksregisternummer3 { get; set; }

        [StringLength(3, ErrorMessage = "Maximum 3"), Required]
        public string Rijksregisternummer4 { get; set; }

        [StringLength(2, ErrorMessage = "Maximum 2"), Required]
        public string Rijksregisternummer5 { get; set; }

        [StringLength(3, ErrorMessage = "Maximum 3")]
        public string Busnummer { get; set; }

        [StringLength(3, ErrorMessage = "Maximum 3"), Required]
        public string Huisnummer { get; set; }

        [StringLength(60, ErrorMessage = "Maximum 60")]
        public string EmailOuders { get; set; }

        [DataType(DataType.Date), Required]
        public DateTime GeboorteDatum { get; set; }

        [StringLength(20, ErrorMessage = "Maximum 20"), Required]
        public string Geslacht { get; set; }

        [EnumDataType(typeof(GraadEnum))]
        public GraadEnum Graad { get; set; }

        [EnumDataType(typeof(LandEnum)), Required]
        public LandEnum Land { get; set; }

        [EnumDataType(typeof(FormuleEnum))]
        public FormuleEnum Lessen { get; set; }

        [StringLength(4, ErrorMessage = "Maximum 4"), Required]
        public string Postcode { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string Stad { get; set; }

        [StringLength(50, ErrorMessage = "Maximum 50"), Required]
        public string StraatNaam { get; set; }

        [EnumDataType(typeof(RolTypeEnum)), Required]
        public RolTypeEnum Roltype { get; set; }

        [StringLength(255, ErrorMessage = "Maximum 255")]
        public string Wachtwoord { get; set; }
        #endregion

        public LidViewModel() {

        }
        public LidViewModel(Lid lid) {
            Id = lid.Id;
            Voornaam = lid.Voornaam;
            Achternaam = lid.Familienaam;
            Email = lid.Email;
            GSM = lid.GSM;
            Telefoon = lid.Telefoon;
            Rijksregisternummer1 = lid.Rijksregisternummer.Substring(0, 2);
            Rijksregisternummer2 = lid.Rijksregisternummer.Substring(3, 2);
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
