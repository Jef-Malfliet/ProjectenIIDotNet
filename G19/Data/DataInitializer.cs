using G19.Filters;
using G19.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [ServiceFilter(typeof(SessionFilter))]
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

            //_context.Sessions.Add(new Models.Session() {
            //    Date = DateTime.Now,
            //    Formule = FormuleEnum.Dinsdag
            //}

            //foreach (Lid li in _context.Leden.ToList()) {

                //int number = (li.Email).GetHashCode() % 97;
                //try {
                //    string va = (li.Voornaam + li.Familienaam);
                //    va = va.Replace(" ", String.Empty);
                //    li.Wachtwoord = va[5].ToString() +va [4].ToString() + va[7].ToString() + Math.Abs(number) + li.Email[2].ToString() + li.Familienaam[1].ToString() + va[1].ToString();
                //    li.Wachtwoord += "!";
                //} catch {
                //    li.Wachtwoord = "Probleem123";
                //}

              //  try {
                //    if (_context.Users.Count(u => u.UserName == li.Email) == 0 && li.Id != 88 && li.Id != 176) {

                //    IdentityUser user = new IdentityUser(li.Email);

                //    await _usermanager.CreateAsync(user, li.Wachtwoord);
                //    await _usermanager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "lid"));
                //    _context.SaveChanges();

                //}
                    //} catch {
                    //    li.Wachtwoord = "Probleem1234";
                    //}


                    //);
                    _context.SaveChanges();

            //}
        }
    }
}

