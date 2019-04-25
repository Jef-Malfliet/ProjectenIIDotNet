using G19.Models;
using G19.Models.Repositories;
using G19.Models.ViewModels;
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

        [HttpPost]
        public IActionResult GeefCommentaar(_CommentsViewModel commentViewModel,int id) {

            _oefeningRepository.AddComment(id, commentViewModel.Comments);
            _oefeningRepository.SaveChanges();
            IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
            return View("Index", oefeningen);

        }
        [Route("Oefening/{graad}")]
        public IActionResult GeefOefeningenPerGraad(string graad) {
            if (graad != "ZWART" && graad != "ALLES") {
                return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad));
            } else if (graad == "ZWART") {
                return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")));
            } else {
                return View(nameof(Index), _oefeningRepository.GetAll());
            }
        }

        [Route("Oefening/{graad}/{id}")]
        public IActionResult GeefOefeningById(int id) {
            Oefening oef = _oefeningRepository.GetById(id);
            return View("_Oefening", oef);
        }

        public ActionResult GeefTextView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Text.cshtml", oef);
        }
        public ActionResult GeefVideoView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Video.cshtml", oef);
        }
        public ActionResult GeefFotoView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Fotos.cshtml", oef);
        }
        public ActionResult GeefCommentView(Oefening oef) {
            return PartialView("~/Views/Oefening/_Comments.cshtml", oef);
        }
    }
}