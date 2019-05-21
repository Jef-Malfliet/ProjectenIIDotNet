using System;
using G19.Filters;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G19.Controllers {
    [Authorize]
    [ServiceFilter(typeof(SessionFilter))]
    public class SessionController : Controller {
        // GET: /<controller>/
        private readonly ILidRepository _lidRepository;
        // private readonly ISessionRepository _sessionRepository;
        public SessionController(ILidRepository lidRepository/*, ISessionRepository sessionRepository*/) {
            _lidRepository = lidRepository;
            //_sessionRepository = sessionRepository;
        }
        [HttpGet]
        public IActionResult Index() {
            return View("Index");
        }

        [HttpGet]
        public IActionResult SessionStateMessage() {
            return View("SessionStateMessage");
        }

        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult StartNieuweSessie(SessionState sessie) {
            if (sessie == null) {
                return View(nameof(Index));
            }
            sessie.ToState(SessionEnum.RegistreerState);
            sessie.vandaag = DateTime.Today.DayOfWeek;
            return View("../Home/Index", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
        }

        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult FakeToday(DayOfWeek dag, SessionState sessie) {
            if (sessie == null) {
                return View(nameof(Index));
            }
            sessie.FakeVandaag(dag);
            return View("../Home/Index", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
        }
        //[HttpGet]
        //public IActionResult MaakNieuweSessie() {
        //    ViewData["formules"] = new SelectList(Enum.GetValues(typeof(FormuleEnum)));
        //    return View("NieuweSessie");
        //}
        //[HttpPost]
        //public IActionResult MaakNieuweSessie(SessionViewModel model) {
        //    Session session = new Session { Formule = model.Formule, Date = model.Date };
        //    _sessionRepository.Add(session);
        //    _sessionRepository.SaveChanges();
        //    SessionState.ToState(SessionEnum.RegistreerState);
        //    // HttpContext.Session.SetString("Sessie", JsonConvert.SerializeObject(session));
        //    return View("../Home/Index",_lidRepository.GetAll().Where(l=>l.Lessen.ToString().Contains(session.Date.Day.ToString())));
        //}
        //[HttpPost]
        //public IActionResult StartBestaandeSessie() {
        //    Session dichtsteSessie = _sessionRepository.GetAll().OrderBy(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds)).FirstOrDefault();
        //    SessionState.ToState(SessionEnum.RegistreerState);
        //    return View("BestaandeSessie", dichtsteSessie);
        //}

        [Authorize(Policy = "Lesgever")]
        public void EndSessionState(SessionState sessie) {
            if (sessie != null) {
                sessie.ToState(SessionEnum.EindState);
            }
        }
    }
}
