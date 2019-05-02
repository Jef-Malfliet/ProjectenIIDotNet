using G19.Models;
using System;
using System.Collections.Generic;

namespace G19Test.Data {
    public class DummyDbContext {
        public Oefening Oefening1 { get; }
        public Oefening Oefening2 { get; }
        public Oefening Oefening3 { get; }

        public Lid Lid1 { get; }
        public Lid Lid2 { get; }
        public Lid Lid3 { get; }
        public Lid Lid4 { get; }
        public Lid Lid5 { get; }
        public Lid Lid6 { get; }
        public Lid Lid7 { get; }

        public DummyDbContext() {
            Oefening1 = new Oefening() {
                Id = 1,
                AantalKeerBekeken = 5,
                Comments = new List<Oefening_Comments>() {
                    new Oefening_Comments() { OefeningId = 1, Comments = "TestComment1Oef1" },
                    new Oefening_Comments() { OefeningId = 1, Comments = "TestComment2Oef1" },
                    new Oefening_Comments() { OefeningId = 1, Comments = "TestComment3Oef1" }
                },
                Graad = GraadEnum.BRUIN,
                Images = new List<Oefening_Images>() {
                    new Oefening_Images() { OefeningId = 1, Images = "PADJE1" },
                    new Oefening_Images() { OefeningId = 1, Images = "PADJE2" }
                },
                Naam = "TestOefening1",
                Uitleg = "TestUitleg1",
                Video = "https://www.youtube.com/watch?v=t7pY-PffCTo"
            };
            Oefening2 = new Oefening() {
                Id = 2,
                AantalKeerBekeken = 15,
                Comments = new List<Oefening_Comments>(),
                Graad = GraadEnum.GEEL,
                Images = new List<Oefening_Images>() {
                    new Oefening_Images() { OefeningId = 2, Images = "PADJE1" }
                },
                Naam = "TestOefening2",
                Uitleg = "TestUitleg2",
                Video = "https://www.youtube.com/watch?v=T-9ZP7eT7oQ"
            };
            Oefening3 = new Oefening() {
                Id = 3,
                AantalKeerBekeken = 60,
                Comments = new List<Oefening_Comments>() {
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment1Oef3" },
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment2Oef3" },
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment3Oef3" },
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment4Oef3" },
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment5Oef3" },
                    new Oefening_Comments() { OefeningId = 3, Comments = "TestComment6Oef3" }
                },
                Graad = GraadEnum.GROEN,
                Images = new List<Oefening_Images>() {
                    new Oefening_Images() { OefeningId = 3, Images = "PADJE1" },
                    new Oefening_Images() { OefeningId = 3, Images = "PADJE2" },
                    new Oefening_Images() { OefeningId = 3, Images = "PADJE3" }
                },
                Naam = "TestOefening3",
                Uitleg = "TestUitleg3",
                Video = "https://www.youtube.com/watch?v=Yeq9vAr037U"
            };

            Lid1 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 16) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 9) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 2) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 26) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 19) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 12) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 5) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 26) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 19) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 12) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 5) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 1, 29) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 1, 22) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 18) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 11) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 4, 4) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 28) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 21) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 14) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 3, 7) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 28) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 21) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 14) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 2, 7) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 1, 31) },
                    new Lid_Aanwezigheden() { LidId = 1, Aanwezigheid = new DateTime(2019, 1, 24) }
                },
                Busnummer = "/",
                Email = "test.lid1@student.hogent.be",
                EmailOuders = "ouders.lid1@telenet.be",
                Familienaam = "Testermans",
                GeboorteDatum = new DateTime(1999, 11, 5),
                Geslacht = "Man",
                Graad = GraadEnum.BLAUW,
                GSM = "0158479515",
                Huisnummer = "15",
                Id = 1,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Dinsdag_Donderdag,
                PostCode = "9320",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Aalst",
                StraatNaam = "Korte Zoutstraat",
                Telefoon = "053584560",
                Voornaam = "Testie",
                Wachtwoord = "P@ssword123"
            };

            Lid2 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 17) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 10) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 3) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 27) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 20) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 13) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 6) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 27) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 20) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 13) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 6) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 1, 30) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 1, 23) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 20) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 13) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 6) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 30) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 23) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 16) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 9) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 3, 2) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 23) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 16) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 9) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 2, 2) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 1, 26) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 1, 19) }
                },
                Busnummer = "/",
                Email = "test.lid2@student.hogent.be",
                EmailOuders = "ouders.lid2@telenet.be",
                Familienaam = "Testerick",
                GeboorteDatum = new DateTime(1985, 3, 29),
                Geslacht = "Vrouw",
                Graad = GraadEnum.GROEN,
                GSM = "0483251489",
                Huisnummer = "147",
                Id = 2,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Woensdag_Zaterdag,
                PostCode = "9000",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Gent",
                StraatNaam = "Sint-bernadettestraat",
                Telefoon = "053584560",
                Voornaam = "Testest",
                Wachtwoord = "P@ssword456"
            };

            Lid3 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 20) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 13) },
                    new Lid_Aanwezigheden() { LidId = 2, Aanwezigheid = new DateTime(2019, 4, 6) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 3, 30) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 3, 23) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 3, 16) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 3, 9) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 3, 2) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 2, 23) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 2, 16) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 2, 9) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 2, 2) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 1, 26) },
                    new Lid_Aanwezigheden() { LidId = 3, Aanwezigheid = new DateTime(2019, 1, 19) }
                },
                Busnummer = "3A",
                Email = "test.lid3@student.hogent.be",
                EmailOuders = "ouders.lid3@telenet.be",
                Familienaam = "Rick",
                GeboorteDatum = new DateTime(2003, 6, 20),
                Geslacht = "Man",
                Graad = GraadEnum.WIT,
                GSM = "0645198705",
                Huisnummer = "14",
                Id = 3,
                Land = LandEnum.Nederland,
                Lessen = FormuleEnum.Zaterdag,
                PostCode = "6200",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Maastricht",
                StraatNaam = "Kuitenbergweg",
                Telefoon = "053584560",
                Voornaam = "Pickle",
                Wachtwoord = "P@ssword789"
            };

            Lid4 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 17) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 10) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 3) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 27) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 20) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 13) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 6) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 27) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 20) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 13) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 6) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 1, 30) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 1, 23) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 20) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 13) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 4, 6) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 30) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 23) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 16) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 9) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 3, 2) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 23) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 16) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 9) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 2, 2) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 1, 26) },
                    new Lid_Aanwezigheden() { LidId = 4, Aanwezigheid = new DateTime(2019, 1, 19) }
                },
                Busnummer = "/",
                Email = "test.lid4@student.hogent.be",
                EmailOuders = "ouders.lid4@telenet.be",
                Familienaam = "Vermeulen",
                GeboorteDatum = new DateTime(1998, 10, 19),
                Geslacht = "Man",
                Graad = GraadEnum.WIT,
                GSM = "0534789456",
                Huisnummer = "33",
                Id = 4,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Woensdag_Zaterdag,
                PostCode = "9850",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Landegem",
                StraatNaam = "Kuitenbergweg",
                Telefoon = "053584560",
                Voornaam = "Nante",
                Wachtwoord = "P@ssword159"
            };

            Lid5 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 4, 20) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 4, 13) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 4, 6) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 3, 30) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 3, 23) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 3, 16) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 3, 9) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 3, 2) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 2, 23) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 2, 16) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 2, 9) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 2, 2) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 1, 26) },
                    new Lid_Aanwezigheden() { LidId = 5, Aanwezigheid = new DateTime(2019, 1, 19) }
                },
                Busnummer = "/",
                Email = "test.lid5@student.hogent.be",
                EmailOuders = "ouders.lid5@telenet.be",
                Familienaam = "Malfliet",
                GeboorteDatum = new DateTime(1999, 10, 24),
                Geslacht = "Man",
                Graad = GraadEnum.WIT,
                GSM = "0458741258",
                Huisnummer = "169",
                Id = 5,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Zaterdag,
                PostCode = "6200",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Hamme",
                StraatNaam = "Kuitenbergweg",
                Telefoon = "053548205",
                Voornaam = "Jef",
                Wachtwoord = "P@ssword258"
            };

            Lid6 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 16) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 9) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 2) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 26) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 19) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 12) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 5) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 26) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 19) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 12) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 5) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 1, 29) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 1, 22) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 18) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 11) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 4, 4) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 28) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2069, 3, 26) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2069, 3, 14) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 3, 7) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 28) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 21) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 14) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 2, 7) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 1, 31) },
                    new Lid_Aanwezigheden() { LidId = 6, Aanwezigheid = new DateTime(2019, 1, 24) }
                },
                Busnummer = "",
                Email = "test.lid6@student.hogent.be",
                EmailOuders = "ouders.lid6@telenet.be",
                Familienaam = "Pessemier",
                GeboorteDatum = new DateTime(1999, 6, 14),
                Geslacht = "Man",
                Graad = GraadEnum.BLAUW,
                GSM = "0645198705",
                Huisnummer = "14",
                Id = 3,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Dinsdag_Donderdag,
                PostCode = "9320",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Erembodegem",
                StraatNaam = "Bertha De Dekenlaan",
                Telefoon = "053689958",
                Voornaam = "Mout",
                Wachtwoord = "P@ssword753"
            };

            Lid7 = new Lid() {
                Aanwezigheden = new List<Lid_Aanwezigheden>() {
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 4, 20) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 4, 13) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 4, 6) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 3, 30) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 3, 23) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 3, 16) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 3, 9) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 3, 2) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 2, 23) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 2, 16) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 2, 9) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 2, 2) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 1, 26) },
                    new Lid_Aanwezigheden() { LidId = 7, Aanwezigheid = new DateTime(2019, 1, 19) }
                },
                Busnummer = "9B",
                Email = "test.lid7@student.hogent.be",
                EmailOuders = "ouders.lid7@telenet.be",
                Familienaam = "Van Canegem",
                GeboorteDatum = new DateTime(1998, 8, 16),
                Geslacht = "Man",
                Graad = GraadEnum.GROEN,
                GSM = "0538114459",
                Huisnummer = "55",
                Id = 7,
                Land = LandEnum.België,
                Lessen = FormuleEnum.Zaterdag,
                PostCode = "9420",
                Rijksregisternummer = "99.06.14-265.78",
                Roltype = RolTypeEnum.Lid,
                Stad = "Zele",
                StraatNaam = "Kuitenbergweg",
                Telefoon = "053584560",
                Voornaam = "Indy",
                Wachtwoord = "P@ssword789"
            };
        }
    }
}
