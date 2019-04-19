using G19.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G19Test.Models {
    public class LidTest {
        private readonly string voornaam = "Mout";
        private readonly string familienaam = "Pessemier";
        private readonly string email = "moutpessemier@hotmail.com";
        private readonly string rijksregisternummer = "99.06.14-265.78";
        private readonly string gsm = "0415446812";
        private readonly string telefoon = "053148621";
        private readonly string postcode = "9320";
        private readonly string huisnummer = "14";
        private readonly string emailOuders = "ouders.mout@telenet.be";
        private readonly string busnummer = "2";
        private readonly DateTime geboorteDatum = new DateTime(1999, 6, 14);
        private readonly string geslacht = "man";
        private readonly GraadEnum graad = GraadEnum.DAN12;
        private readonly LandEnum land = LandEnum.België;
        private readonly FormuleEnum formule = FormuleEnum.Woensdag;
        private readonly string stad = "Erembodegem";
        private readonly string straatNaam = "GelbertStraat";
        private readonly string wachtwoord = "DitIsZekerMijnEchtWachtWoord!";
        private readonly RolTypeEnum Roltype = RolTypeEnum.Beheerder;


        [Fact]
        public void TestConstructorAlleWaardenValues() {
            Lid lid = new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            };
            Assert.Equal(voornaam, lid.Voornaam);
            Assert.Equal(familienaam, lid.Familienaam);
            Assert.Equal(email, lid.Email);
            Assert.Equal(rijksregisternummer, lid.Rijksregisternummer);
            Assert.Equal(gsm, lid.GSM);
            Assert.Equal(telefoon, lid.Telefoon);
            Assert.Equal(postcode, lid.PostCode);
            Assert.Equal(huisnummer, lid.Huisnummer);
            Assert.Equal(emailOuders, lid.EmailOuders);
            Assert.Equal(busnummer, lid.Busnummer);
            Assert.Equal(geboorteDatum, lid.GeboorteDatum);
            Assert.Equal(geslacht, lid.Geslacht);
            Assert.Equal(graad, lid.Graad);
            Assert.Equal(land, lid.Land);
            Assert.Equal(formule, lid.Lessen);
            Assert.Equal(stad, lid.Stad);
            Assert.Equal(straatNaam, lid.StraatNaam);
            Assert.Equal(wachtwoord, lid.Wachtwoord);
            Assert.Equal(Roltype, lid.Roltype);
        }

        [Fact]
        public void TestConstructorSommigeWaardenNull() {//need fix, don't know how yet
            Lid lid = new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = null,
                EmailOuders = null,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            };
            Assert.Equal(voornaam, lid.Voornaam);
            Assert.Equal(familienaam, lid.Familienaam);
            Assert.Equal(email, lid.Email);
            Assert.Equal(rijksregisternummer, lid.Rijksregisternummer);
            Assert.Equal(gsm, lid.GSM);
            Assert.Equal(telefoon, lid.Telefoon);
            Assert.Equal(postcode, lid.PostCode);
            Assert.Equal(huisnummer, lid.Huisnummer);
            Assert.Equal("/", lid.EmailOuders);
            Assert.Equal("/", lid.Busnummer);
            Assert.Equal(geboorteDatum, lid.GeboorteDatum);
            Assert.Equal(geslacht, lid.Geslacht);
            Assert.Equal(graad, lid.Graad);
            Assert.Equal(land, lid.Land);
            Assert.Equal(formule, lid.Lessen);
            Assert.Equal(stad, lid.Stad);
            Assert.Equal(straatNaam, lid.StraatNaam);
            Assert.Equal(wachtwoord, lid.Wachtwoord);
            Assert.Equal(Roltype, lid.Roltype);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        public void TestFoutieveWaardenVoornaam(string voornaam) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        public void TestFoutieveWaardenFamilienaam(string familienaam) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("test@test")]
        [InlineData("test.com")]
        [InlineData("test@.com")]
        [InlineData("@test.com")]
        [InlineData("-test@test.com")]
        [InlineData("_test@test.com")]
        [InlineData("*test@test.com")]
        [InlineData(".test@test.com")]
        [InlineData("$test@test.com")]
        [InlineData("@test@test.com")]
        [InlineData("!test@test.com")]
        [InlineData("test&@test.com")]
        [InlineData("test@test&.com")]
        public void TestFoutieveWaardenEmail(string email) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData("test@test")]
        [InlineData("test.com")]
        [InlineData("test@.com")]
        [InlineData("@test.com")]
        [InlineData("-test@test.com")]
        [InlineData("_test@test.com")]
        [InlineData("*test@test.com")]
        [InlineData(".test@test.com")]
        [InlineData("$test@test.com")]
        [InlineData("@test@test.com")]
        [InlineData("!test@test.com")]
        [InlineData("test&@test.com")]
        [InlineData("test@test&.com")]
        public void TestFoutieveWaardenOuderEmail(string emailOuders) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("015788441")]
        [InlineData("01578844189")]
        [InlineData("479554479")]
        [InlineData("0049744552")]
        [InlineData("47911447945")]
        [InlineData("00479554479735")]
        public void TestFoutieveWaardenGSM(string gsm) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData("05347100")]
        [InlineData("53471000")]
        [InlineData("0534710000")]
        [InlineData("00325371489")]
        [InlineData("325371489098")]
        [InlineData("0032571489405")]
        public void TestFoutieveWaardenTelefoon(string telefoon) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("99061426578")]
        [InlineData("99.06.14-265.77")]//foute contRoltypee
        [InlineData("99&06.14-265.77")]
        [InlineData("99.06.14.265.77")]
        public void TestFoutieveWaardenRijksregisternummer(string rijksregisternummer) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("0320")]
        [InlineData("932")]
        [InlineData("93205")]
        [InlineData("9320A")]
        [InlineData("B320")]
        public void TestFoutieveWaardenPostcode(string postcode) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("                ")]
        [InlineData("\t\n\r")]
        [InlineData("A")]
        [InlineData("1A1")]
        [InlineData("A1")]
        public void TestFoutieveWaardenHuisnummer(string huisnummer) {
            Assert.Throws<ArgumentException>(() => new Lid() {
                Voornaam = voornaam,
                Familienaam = familienaam,
                Email = email,
                Busnummer = busnummer,
                EmailOuders = emailOuders,
                Lessen = formule,
                GeboorteDatum = geboorteDatum,
                Geslacht = geslacht,
                Graad = graad,
                GSM = gsm,
                Huisnummer = huisnummer,
                Land = land,
                Id = 1,
                PostCode = postcode,
                Rijksregisternummer = rijksregisternummer,
                Roltype = Roltype,
                Stad = stad,
                StraatNaam = straatNaam,
                Telefoon = telefoon,
                Wachtwoord = wachtwoord
            });
        }
    }
}
