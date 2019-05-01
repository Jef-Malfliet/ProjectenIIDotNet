using Microsoft.AspNetCore.Mvc;
using G19.Models;
using G19.Models.Repositories;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace G19.Controllers {
    [Authorize(Policy = "Lesgever")]
    public class HomeController : Controller {
        private readonly ILidRepository _lidRepository;

        public HomeController(ILidRepository lidRepository) {
            _lidRepository = lidRepository;
        }

        [Authorize]
        public IActionResult Index() {
            return View(_lidRepository.GetAll());
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
            var leden = _lidRepository.GetByGraad(graad);
            return View(nameof(Index), _lidRepository.GetByGraad(graad));
        }
        
        public IActionResult GeefAanwezigenVandaag() {
            var aanwezigeLedenVandaag = _lidRepository.GetAll().Where(l => l.benIkAanwezigVandaag());
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
