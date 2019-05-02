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
    public class HomeController : Controller {
        private readonly ILidRepository _lidRepository;
        private readonly ISessionRepository _sessionRepository;
        public HomeController(ILidRepository lidRepository, ISessionRepository sessionRepository) {
            _lidRepository = lidRepository;
            _sessionRepository = sessionRepository;
        }

        public IActionResult Index() {
            if (SessionState.AanwezigheidRegistrerenState()) {
                //Session session = GeefHuidgeSessie();
                //return View(_lidRepository.GetByFormule(session.Formule));

               
                return View(_lidRepository.GetLedenInFormuleOfDay(SessionState.vandaag));
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return View("~/Views/Session/SessionStateMessage.cshtml");

            }
        }

        public IActionResult GeefAlleLeden() {
            if (SessionState.AanwezigheidRegistrerenState()) {
                return View(nameof(Index), _lidRepository.GetAll());
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return View("~/Views/Session/SessionStateMessage.cshtml");

            }
        }
        
        [Route("Home/{graad}")]
        public IActionResult GeefAanwezighedenPerGraad(string graad) {
            if (SessionState.AanwezigheidRegistrerenState()) {
                //if (graad != "ZWART" && graad != "ALLES") {
                //    // return View(nameof(Index), _lidRepository.GetByGraad(graad));
                //    return View(nameof(Index), _lidRepository.GetAll().Where(lid => lid.Graad.ToString() == graad));
                //} else if (graad == "ZWART") {
                //    return View(nameof(Index), _lidRepository.GetAll().Where(lid => lid.Graad.ToString().StartsWith("DAN")));
                //}else{
                //    return View(nameof(Index), _lidRepository.GetAll());
                //}
                //  Session session = GeefHuidgeSessie();
                //var leden = _lidRepository.GetByGraad(graad,session.Formule);
                var leden = _lidRepository.GetByGraadEnFormuleOfDay(graad,SessionState.vandaag);

                return View(nameof(Index), leden);
            } else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return View("~/Views/Session/SessionStateMessage.cshtml");

            }
        }

        public IActionResult GeefAanwezigenVandaag() {
            if (SessionState.OefeningenBekijkenState()) {
             //   Session session = GeefHuidgeSessie();
                var aanwezigeLedenVandaag = _lidRepository.GetAll().Where(l => l.benIkAanwezigVandaag());
                return View(nameof(GeefAanwezigenVandaag), aanwezigeLedenVandaag);
            }
            else {
                TempData["SessionStateMessage"] = "Je moet alle aanwezigen doorgeven door op de knop 'Aanwezigheden zijn geregistreerd' te drukken.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }

        }
        public IActionResult RegistreerAanwezigheid(int id) {
            if (SessionState.AanwezigheidRegistrerenState()) {
                var lid = _lidRepository.GetById(id);
                _lidRepository.RegisteerAanwezigheid(lid);
                _lidRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }

        }

        public IActionResult RegistreerExtraLid(string voornaam, string familienaam) {
            if (SessionState.AanwezigheidRegistrerenState()) {
                var lid = _lidRepository.GetByNames(voornaam, familienaam);
                _lidRepository.RegisteerAanwezigheid(lid);
                _lidRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else {
                TempData["SessionStateMessage"] = "Alle aanwezigheden zijn reeds doorgegeven.";
                return View("~/Views/Session/SessionStateMessage.cshtml");
            }
        }

        public IActionResult ToOefeningState() {
            SessionState.ToState(SessionEnum.OefeningState);
            return RedirectToAction(nameof(GeefAanwezigenVandaag));
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
