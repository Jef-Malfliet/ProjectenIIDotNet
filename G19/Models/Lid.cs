using System;
using System.Text.RegularExpressions;

namespace G19.Models {
    public class Lid {

        #region Fields
        private string _voornaam;
        private string _familienaam;
        private string _email;
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
        #endregion

        #region Constructors
        public Lid() {

        }

        //public Lid(string voornaam, string familienaam, string email) {
        //    Voornaam = voornaam;
        //    Familienaam = familienaam;
        //    Email = email;
        //}
        #endregion

        #region Methods

        #endregion
    }
}
