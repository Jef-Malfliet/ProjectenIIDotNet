using G19.Filters;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace G19.Controllers {
    [Authorize(Policy = "Lesgever")]
    [ServiceFilter(typeof(SessionFilter))]
    public class HomeController : Controller {
        private readonly ILidRepository _lidRepository;
        //private readonly ISessionRepository _sessionRepository;
        public HomeController(ILidRepository lidRepository/*, ISessionRepository sessionRepository*/) {
            _lidRepository = lidRepository;
            // _sessionRepository = sessionRepository;
        }

        public IActionResult Index(SessionState sessie) {
            if(sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.AanwezigheidRegistrerenState()) {
                //Session session = GeefHuidgeSessie();
                //return View(_lidRepository.GetByFormule(session.Formule));
                return View("Index",_lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        public IActionResult GeefAlleLeden(SessionState sessie) {
            if(sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.AanwezigheidRegistrerenState()) {
                return View(nameof(Index), _lidRepository.GetAll());
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        [Route("Home/{graad}")]
        public IActionResult GeefAanwezighedenPerGraad(string graad,SessionState sessie) {
            if(graad == null || sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.AanwezigheidRegistrerenState()) {
                var leden = _lidRepository.GetByGraadEnFormuleOfDay(graad, sessie.vandaag);
                return View(nameof(Index), leden);
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        public IActionResult GeefAanwezigenVandaag(SessionState sessie) {
            if (sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.OefeningenBekijkenState()) {
                //   Session session = GeefHuidgeSessie();
                var aanwezigeLedenVandaag = _lidRepository.GetAll().Where(l => l.BenIkAanwezigVandaag());
                return View(nameof(GeefAanwezigenVandaag), aanwezigeLedenVandaag);
            } else {
                TempData["SessionStateMessage"] = "Je moet alle aanwezigen doorgeven door op de knop 'Aanwezigheden zijn geregistreerd' te drukken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }

        }
        public IActionResult RegistreerAanwezigheid(int id,SessionState sessie) {
            if (id < 0 || sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.AanwezigheidRegistrerenState()) {
                var lid = _lidRepository.GetById(id);
                lid.Aanwezigheden.Add(new Lid_Aanwezigheden { Aanwezigheid = DateTime.Now, LidId = id });
                _lidRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        public IActionResult RegistreerExtraLid(string voornaam, string familienaam,SessionState sessie) {
            if(voornaam == null || familienaam == null|| sessie == null) {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (sessie.AanwezigheidRegistrerenState()) {
                var lid = _lidRepository.GetByNames(voornaam, familienaam);
                _lidRepository.RegisteerAanwezigheid(lid);
                _lidRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        public IActionResult ToOefeningState(string code,SessionState sessie) {
            if(code == null || sessie == null) {
                return View("Index", _lidRepository.GetLedenInFormuleOfDay(DateTime.Today.DayOfWeek));
            }
            if (code == "1234") {
                sessie.ToState(SessionEnum.OefeningState);
                return RedirectToAction(nameof(GeefAanwezigenVandaag));
            }
            return View("Index", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
        }

        //private Session GeefHuidgeSessie() {
        //    //return JsonConvert.DeserializeObject<Session>(HttpContext.Session.GetString("Sessie"));
        //    var session = _sessionRepository.GetAll().Select(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds));

        //    Session ses = _sessionRepository.GetAll().OrderBy(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds)).FirstOrDefault();
        //    return ses;
        //}

        //public IActionResult Privacy() {
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error() {
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
