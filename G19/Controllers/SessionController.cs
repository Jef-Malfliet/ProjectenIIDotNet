using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G19.Controllers {
    [Authorize(Policy = "Lesgever")]
    public class SessionController : Controller {
        // GET: /<controller>/
        private readonly ILidRepository _lidRepository;
        private readonly ISessionRepository _sessionRepository;
        public SessionController(ILidRepository lidRepository, ISessionRepository sessionRepository) {
            _lidRepository = lidRepository;
            _sessionRepository = sessionRepository;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
        [HttpGet]
        public IActionResult MaakNieuweSessie() {
            ViewData["formules"] = new SelectList(Enum.GetValues(typeof(FormuleEnum)));
            return View("NieuweSessie");
        }
        [HttpPost]
        public IActionResult MaakNieuweSessie(SessionViewModel model) {
            Session session = new Session { Formule = model.Formule, Date = model.Date };
            _sessionRepository.Add(session);
            return View("../Home/Index",_lidRepository.GetByFormule(session.Formule));
        }
        [HttpGet]
        public IActionResult StartBestaandeSessie() {
            return View("BestaandeSessie");
        }
    }
}
