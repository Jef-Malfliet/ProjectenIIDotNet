using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace G19.Models {
    public class Lid {

        #region Fields
        private string _voornaam;
        private string _familienaam;
        private string _email;
        private string _rijksregisternummer;
        private string _gsm;
        private string _telefoon;
        private string _postcode;
        private string _huisnummer;
        private string _emailOuders;
        private string _busnummer;
        #endregion

        #region Properties
        public int Id { get; set; }
        public string Voornaam {
            get {
                return _voornaam;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Voornaam mag niet leeg zijn.");
                }
                _voornaam = value;
            }
        }
        public string Familienaam {
            get {
                return _familienaam;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Familienaam mag niet leeg zijn.");
                }
                _familienaam = value;
            }
        }
        public string Email {
            get {
                return _email;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Email mag niet leeg zijn mag niet leeg zijn.");
                }
                Regex regex = new Regex(@"^([a-zA-Z0-9éèà]+[a-zA-Z0-9.\-éèàïëöüäîôûêâù]*)@([a-zA-Z]+)[.]([a-z]+)([.][a-z]+)*$");
                Match match = regex.Match(value);
                if (!match.Success) {
                    throw new ArgumentException("Email voldoet niet aan de voorwaarden.");
                }
                _email = value;
            }
        }

        public string GSM {
            get {
                return _gsm;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("GSM mag niet leeg zijn mag niet leeg zijn.");
                }
                Regex regex = new Regex(@"^((0[1-9][0-9]{8})|(0{2}[1-9][0-9]{8}))$");
                Match match = regex.Match(value);
                if (!match.Success) {
                    throw new ArgumentException("GSM voldoet niet aan de voorwaarden.");
                }
                _gsm = value;
            }
        }
        public string Telefoon {
            get {
                return _telefoon;
            }
            set {
                if (value != null) {
                    Regex regex = new Regex(@"^((0[1-9]{1}[0-9]{7})|(0{2}[1-9]{1}[0-9]{9}))$");
                    Match match = regex.Match(value);
                    if (!match.Success) {
                        throw new ArgumentException("Telefoon voldoet niet aan de voorwaarden.");
                    }
                }
                _telefoon = value;
            }
        }
        public string Rijksregisternummer {
            get {
                return _rijksregisternummer;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Rijksregisternummer mag niet leeg zijn mag niet leeg zijn.");
                }
                Regex regex = new Regex(@"^([0-9]{2}).(0[1-9]|1[0-2]).((0[1-9])|(1[0-9])|(2[0-9]|3[0-1]))-([0-9]{3}).([0-9]{2})$");
                Match match = regex.Match(value);
                StringBuilder sb = new StringBuilder();
                sb.Append(match.Groups[1]).Append(match.Groups[2]).Append(match.Groups[3]).Append(match.Groups[7]);
                bool tryParseGetal = Int32.TryParse(sb.ToString(), out int getal);
                bool tryParseControle = Int32.TryParse(match.Groups[8].ToString(), out int controlecijfer);
                int modulo = getal % 97;
                if (!match.Success || !tryParseControle || !tryParseGetal || 97 - modulo != controlecijfer) {
                    throw new ArgumentException("Rijksregisternummer voldoet niet aan de voorwaarden.");
                }
                _rijksregisternummer = value;
            }
        }
        public string Busnummer {
            get {
                return _busnummer;
            }
            set {
                if (value == "/" || value == null)
                    _busnummer = "/";
                else
                    _busnummer = value;


            }
        }
        public string Huisnummer {
            get {
                return _huisnummer;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Huisnummer mag niet leeg zijn mag niet leeg zijn.");
                }
                Regex regex = new Regex(@"^[0-9]+[a-zA-Z]*$");
                Match match = regex.Match(value);
                if (!match.Success) {
                    throw new ArgumentException("Huisnummer voldoet niet aan de voorwaarden.");
                }
                _huisnummer = value;
            }
        }
        public string EmailOuders {
            get {
                return _emailOuders;
            }
            set {
                if (value == "/" || value == null)
                    _emailOuders = "/";
                else {
                    Regex regex = new Regex(@"^([a-zA-Z0-9éèà]+[a-zA-Z0-9.\-éèàïëöüäîôûêâù]*)@([a-zA-Z]+)[.]([a-z]+)([.][a-z]+)*$");
                    Match match = regex.Match(value);

                    if (!match.Success) {
                        throw new ArgumentException("Email voldoet niet aan de voorwaarden.");
                    }
                    _emailOuders = value;
                }


            }
        }
        public DateTime GeboorteDatum { get; set; }
        public string Geslacht { get; set; }
        public GraadEnum Graad { get; set; }
        public LandEnum Land { get; set; }
        public FormuleEnum Lessen { get; set; }
        public string PostCode {
            get {
                return _postcode;
            }
            set {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) {
                    throw new ArgumentException("Postcode mag niet leeg zijn mag niet leeg zijn.");
                }
                Regex regex = new Regex(@"^[1-9][0-9]{3}$");
                Match match = regex.Match(value);
                if (!match.Success) {
                    throw new ArgumentException("Postcode voldoet niet aan de voorwaarden.");
                }
                _postcode = value;
            }
        }
        public string Stad { get; set; }
        public string StraatNaam { get; set; }
        public RolTypeEnum Roltype { get; set; }
        public string Wachtwoord { get; set; }
        public IList<Lid_Aanwezigheden> Aanwezigheden { get; set; }
        #endregion

        #region Constructors
        public Lid() { }
        #endregion

        #region Methods
        public bool BenIkAanwezigVandaag() {
            return Aanwezigheden.Any(aanwezigheid => aanwezigheid.Aanwezigheid.Date == DateTime.Today);
        }
        public int GeefGraadInGetal() {
            switch (Graad) {
                case GraadEnum.WIT: return 0;
                case GraadEnum.GEEL: return 1;
                case GraadEnum.ORANJE: return 2;
                case GraadEnum.GROEN: return 3;
                case GraadEnum.BLAUW: return 4;
                case GraadEnum.BRUIN: return 5;
                default: return 6;
                    #endregion
            }
        }
    }
}
