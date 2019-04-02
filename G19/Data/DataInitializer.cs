using G19.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_context.Database.EnsureCreated()) {
                Lid INDY = new Lid() {
                    Id = 500,
                    Busnummer = "0101",
                    Email = "Indyvancanegem@hotmail.com",
                    Familienaam = "Van CANEGEM",
                    Formule = FormuleEnum.Dinsdag,
                    GeboorteDatum = DateTime.Now,
                    Geslacht = "MAN",
                    Graad = GraadEnum.Bbruin,
                    GSM = "0483060043",
                    Land = LandEnum.België,
                    PostCode = "9240",
                    Rijksregisternummer = "98.08.16-183.93",
                    Rol = RolTypeEnum.Beheerder,
                    Huisnummer = "132",
                    Stad = "Zele",
                    StraatNaam = "Ommegangstraat 132",
                    Voornaam = "KELLYYY",
                    Wachtwoord = "P@ssword1111",
                    Telefoon = "053444541",
                    EmailOuders = "test@hotmail.com"
                };

                _context.Leden.Add(INDY);
            }
        }
    }
}
