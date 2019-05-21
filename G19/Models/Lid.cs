using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json;
using G19.Models.ViewModels;

namespace G19.Models {
    [JsonObject(MemberSerialization.OptIn)]
    public class Lid {

        #region Fields
        [JsonProperty]
        private string _voornaam;
        [JsonProperty]
        private string _familienaam;
        [JsonProperty]
        private string _email;
        [JsonProperty]
        private string _rijksregisternummer;
        [JsonProperty]
        private string _gsm;
        [JsonProperty]
        private string _telefoon;
        [JsonProperty]
        private string _postcode;
        [JsonProperty]
        private string _huisnummer;
        [JsonProperty]
        private string _emailOuders;
        [JsonProperty]
        private string _busnummer;
        #endregion

        #region Properties
        [JsonProperty]
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
        [JsonProperty]
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
        [JsonProperty]
        public string Stad { get; set; }
        [JsonProperty]
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

        public void MapLidViewModelToLidInSession(LidViewModelSession LidViewModelInSession, Lid lid) {
            lid.Email = LidViewModelInSession.Email;
            lid.GSM = LidViewModelInSession.GSM;
            lid.Telefoon = LidViewModelInSession.Telefoon;
            lid.Busnummer = LidViewModelInSession.Busnummer;
            lid.Huisnummer = LidViewModelInSession.Huisnummer;
            lid.EmailOuders = LidViewModelInSession.EmailOuders;
            lid.PostCode = LidViewModelInSession.Postcode;
            lid.Stad = LidViewModelInSession.Stad;
            lid.StraatNaam = LidViewModelInSession.StraatNaam;
        }

        public void MapLidViewModelToLid(LidViewModel LidViewModel, Lid lid) {
            lid.Voornaam = LidViewModel.Voornaam;
            lid.Familienaam = LidViewModel.Achternaam;
            lid.Rijksregisternummer = LidViewModel.Rijksregisternummer1 + "." + LidViewModel.Rijksregisternummer2 + "."
                                 + LidViewModel.Rijksregisternummer3 + "-" + LidViewModel.Rijksregisternummer4 + "." + LidViewModel.Rijksregisternummer5;
            lid.GeboorteDatum = LidViewModel.GeboorteDatum;
            lid.Geslacht = LidViewModel.Geslacht;
            lid.Land = LidViewModel.Land;

            lid.Email = LidViewModel.Email;
            lid.GSM = LidViewModel.GSM;
            lid.Telefoon = LidViewModel.Telefoon;
            lid.Busnummer = LidViewModel.Busnummer;
            lid.Huisnummer = LidViewModel.Huisnummer;
            lid.EmailOuders = LidViewModel.EmailOuders;
            lid.PostCode = LidViewModel.Postcode;
            lid.Stad = LidViewModel.Stad;
            lid.StraatNaam = LidViewModel.StraatNaam;

            // lid.Lessen = LidViewModel.Lessen;
            // lid.Graad = LidViewModel.Graad;
            // lid.Roltype = LidViewModel.Roltype;
            // lid.Wachtwoord = LidViewModel.Wachtwoord;
            //lid.Id = LidViewModel.Id;

        }
    }
}
