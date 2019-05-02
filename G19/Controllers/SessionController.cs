using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
            _sessionRepository.SaveChanges();
            SessionState.ToState(SessionEnum.RegistreerState);
            // HttpContext.Session.SetString("Sessie", JsonConvert.SerializeObject(session));
            return View("../Home/Index",_lidRepository.GetAll().Where(l=>l.Lessen.ToString().Contains(session.Date.Day.ToString())));
        }
        [HttpGet]
        public IActionResult StartBestaandeSessie() {
            Session dichtsteSessie = _sessionRepository.GetAll().OrderBy(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds)).FirstOrDefault();
            SessionState.ToState(SessionEnum.RegistreerState);
            return View("BestaandeSessie", dichtsteSessie);
        }

        public void EndSessionState() {
            SessionState.ToState(SessionEnum.EindState);
        }
    }
}
