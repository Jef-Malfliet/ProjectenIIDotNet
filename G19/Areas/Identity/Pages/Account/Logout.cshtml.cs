using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace G19.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string code, string currentUrl, string returnUrl = null)
        {

                if (!_signInManager.Context.User.HasClaim((c => c.Value == "lesgever")) || code == "1234") {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                if (returnUrl != null) {
                    return LocalRedirect(returnUrl);
                } else {
                    return Page();
                }
            } else {
                return LocalRedirect(currentUrl);
            }

        }
    }
}