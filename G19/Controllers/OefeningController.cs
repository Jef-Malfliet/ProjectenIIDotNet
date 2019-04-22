using G19.Models;
using G19.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G19.Controllers {
    public class OefeningController : Controller {
        private readonly IOefeningRepository _oefeningRepository;

        public OefeningController(IOefeningRepository oefeningRepository) {
            _oefeningRepository = oefeningRepository;
        }
        public IActionResult Index() {
            IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
            ViewData["Graden"] = new List<GraadEnum>(new HashSet<GraadEnum>((GraadEnum[])Enum.GetValues(typeof(GraadEnum))));
            return View(oefeningen);
        }

        public IActionResult geefCommentaar(int id, string commentaar) {

            _oefeningRepository.AddComment(id, commentaar);
            IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
            return View("Index", oefeningen);

        }
        [Route("Oefening/{graad}")]
        public IActionResult GeefOefeningenPerGraad(GraadEnum graad) {
            
            IEnumerable<Oefening> oefeningenPerGraad = _oefeningRepository.GetOefeningenPerGraad(graad);
            return View("Index", oefeningenPerGraad);
        }
        [Route("Oefening/{graad}/{id}")]
        public IActionResult GeefOefeningById(int id) {
            Oefening oef = _oefeningRepository.GetById(id);
            return View("_Oefening", oef);
        }

        public ActionResult geefTextView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Text.cshtml", oef);
        }
        public ActionResult geefVideoView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Video.cshtml", oef);
        }
        public ActionResult geefFotoView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Fotos.cshtml", oef);
        }
        public ActionResult geefCommentView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Comments.cshtml", oef);
        }
    }
}