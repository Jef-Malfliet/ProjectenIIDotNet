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
            return View("~/Views/Oefening/Comments.cshtml", _oefeningRepository.GetById(id));

        }
        [Route("Oefening/{graad}")]
        public IActionResult GeefOefeningenPerGraad(string graad) {
            if (graad != "ZWART" && graad != "ALLES") {
                return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad).OrderBy(oef => oef.Graad));
            } else if (graad == "ZWART") {
                return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")).OrderBy(oef => oef.Graad));
            } else {
                return View(nameof(Index), _oefeningRepository.GetAll().OrderBy(oef => oef.Graad));
            }
        }

        [Route("Oefening/{graad}/{id}")]
        public IActionResult GeefOefeningById(int id) {
            Oefening oef = _oefeningRepository.GetById(id);
            return View("_Oefening", oef);
        }

        public ActionResult GeefTextView(int Id) {
            return View("~/Views/Oefening/Text.cshtml", _oefeningRepository.GetById(Id));
        }
        public ActionResult GeefVideoView(int Id) {
            return View("~/Views/Oefening/Video.cshtml", _oefeningRepository.GetById(Id));
        }
        public ActionResult GeefFotoView(int Id) {
            return View("~/Views/Oefening/Fotos.cshtml", _oefeningRepository.GetById(Id));
        }
        public ActionResult GeefCommentView(int Id) {
            return View("~/Views/Oefening/Comments.cshtml", _oefeningRepository.GetById(Id));
        }
    }
}