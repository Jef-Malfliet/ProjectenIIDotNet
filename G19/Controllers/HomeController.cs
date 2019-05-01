using Microsoft.AspNetCore.Mvc;
using G19.Models;
using G19.Models.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            Session session = GeefHuidgeSessie();
            return View(_lidRepository.GetByFormule(session.Formule));
        }
        private Session GeefHuidgeSessie() {
            //return JsonConvert.DeserializeObject<Session>(HttpContext.Session.GetString("Sessie"));
            var session = _sessionRepository.GetAll().Select(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds));
            
            Session ses =  _sessionRepository.GetAll().OrderBy(s => Math.Abs(DateTime.Now.Subtract(s.Date).TotalSeconds)).FirstOrDefault();
            return ses;
        }
        [Route("Home/{graad}")]
        public IActionResult GeefAanwezighedenPerGraad(string graad) {
            //if (graad != "ZWART" && graad != "ALLES") {
            //    // return View(nameof(Index), _lidRepository.GetByGraad(graad));
            //    return View(nameof(Index), _lidRepository.GetAll().Where(lid => lid.Graad.ToString() == graad));
            //} else if (graad == "ZWART") {
            //    return View(nameof(Index), _lidRepository.GetAll().Where(lid => lid.Graad.ToString().StartsWith("DAN")));
            //}else{
            //    return View(nameof(Index), _lidRepository.GetAll());
            //}
            Session session = GeefHuidgeSessie();
            //var leden = _lidRepository.GetByGraad(graad,session.Formule);
            var leden = _lidRepository.GetByGraadEnFormule(graad,session.Formule);

            return View(nameof(Index),leden);
        }
        
        public IActionResult GeefAanwezigenVandaag() {
            Session session = GeefHuidgeSessie();
            var aanwezigeLedenVandaag = _lidRepository.GetAll().Where(l => l.benIkAanwezigVandaag() && l.Lessen.Equals(session.Formule));
            return View(nameof(GeefAanwezigenVandaag), aanwezigeLedenVandaag);

        }
        public IActionResult RegistreerAanwezigheid(int id) {
            var lid = _lidRepository.GetById(id);
            _lidRepository.RegisteerAanwezigheid(lid);
            _lidRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RegistreerExtraLid(string voornaam, string familienaam) {
            var lid = _lidRepository.GetByNames(voornaam,familienaam);
            _lidRepository.RegisteerAanwezigheid(lid);
            _lidRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Privacy() {
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error() {
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
