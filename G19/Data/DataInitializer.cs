using G19.Models;
using Microsoft.AspNetCore.Identity;
using System;
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

        public async Task InitializeData() {
            _usermanager.Options.Password.RequireDigit = false;
            _usermanager.Options.Password.RequiredLength = 1;
            _usermanager.Options.Password.RequiredUniqueChars = 1;
            _usermanager.Options.Password.RequireLowercase = false;
            _usermanager.Options.Password.RequireNonAlphanumeric = false;
            _usermanager.Options.Password.RequireUppercase = false;

            foreach (var lid in _context.Leden) {
                var createdUser = _usermanager.Users.FirstOrDefault(u => u.UserName == lid.Email);
                if ( createdUser == null) {
                    IdentityUser newIdentityUser = new IdentityUser(lid.Email);
                    await _usermanager.CreateAsync(newIdentityUser, lid.Wachtwoord);
                    if (lid.Roltype == RolTypeEnum.Lesgever) {
                        await _usermanager.AddClaimAsync(newIdentityUser, new Claim(ClaimTypes.Role, "lesgever"));
                    }
                    await _usermanager.AddClaimAsync(newIdentityUser, new Claim(ClaimTypes.Role, "lid"));
                }
                else {
                    await _usermanager.RemovePasswordAsync(createdUser);
                    await _usermanager.AddPasswordAsync(createdUser, lid.Wachtwoord);
                    await _usermanager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Role, "lid"));
                }
            }
            _context.SaveChanges();
        }

        private static string GetStringSha256Hash(string text) {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed()) {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

    }
}


