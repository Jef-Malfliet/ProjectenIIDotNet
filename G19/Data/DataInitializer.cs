﻿using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace G19.Data {
    public class DataInitializer {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _usermanager;

        public DataInitializer(ApplicationDbContext context, UserManager<IdentityUser> usermanager) {
            this._context = context;
            this._usermanager = usermanager;
        }

        public async Task InitializeData() {

            //Oefening oefening = new Oefening() {
            //    AantalKeerBekeken = 5,
            //    Graad = GraadEnum.BLAUW,
            //    Id = 10010,
            //    Naam = "Nantes oefening",
            //    Comments = new List<Oefening_Comments>() { new Oefening_Comments() { OefeningId = 10010, Comments = "comment1" }, new Oefening_Comments() { OefeningId = 10010, Comments = "comment2" } },
            //    Images = new List<Oefening_Images>() { new Oefening_Images() { OefeningId = 10010, Images = "PADJE" }, new Oefening_Images() { OefeningId = 10010, Images = "PADJE2" } },
            //    //Comments = new List<string>() { "fqsfsdf","qsfsd"},
            //    //Images = new List<string>() {  "fqsfsdf", "qsfsd"  },

            //    Uitleg = "uitlegoefening",
            //    Video = "https://www.youtube.com/embed/t7pY-PffCTo"

            //};
            //_context.Oefeningen.Add(oefening);

            //Lid INDY = new Lid() {
            //    Id = 500,
            //    Busnummer = "01",
            //    Email = "Indyvancanegem@hotmail.com",
            //    Familienaam = "Van CANEGEM",
            //    Lessen = FormuleEnum.Dinsdag,
            //    GeboorteDatum = DateTime.Now,
            //    Geslacht = "MAN",
            //    Graad = GraadEnum.BRUIN,
            //    GSM = "0483060043",
            //    Land = LandEnum.België,
            //    PostCode = "9240",
            //    Rijksregisternummer = "98.08.16-183.93",
            //    Roltype = RolTypeEnum.Beheerder,
            //    Huisnummer = "132",
            //    Stad = "Zele",
            //    StraatNaam = "Ommegangstraat 132",
            //    Voornaam = "KELLYYY",
            //    Wachtwoord = "P@ssword1111",
            //    Telefoon = "053444541",
            //    EmailOuders = "test@hotmail.com"
            //};

            //_context.Leden.Add(INDY);
            //IdentityUser user = new IdentityUser("admin2@hotmail.com");
            //await _usermanager.CreateAsync(user, "P@ssword1111");
            //await _usermanager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "lesgever"));
            _context.SaveChanges();

        }
    }
}

