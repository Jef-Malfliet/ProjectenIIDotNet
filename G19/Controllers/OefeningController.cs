using G19.Filters;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace G19.Controllers {
    [Authorize]
    [ServiceFilter(typeof(SessionFilter))]
    public class OefeningController : Controller {
        private readonly IOefeningRepository _oefeningRepository;
        private readonly ILidRepository _lidRepository;
        private readonly IMailRepository _mailRepository;

        public OefeningController(IOefeningRepository oefeningRepository, ILidRepository lidRepository, IMailRepository mailRepository) {
            _oefeningRepository = oefeningRepository;
            _lidRepository = lidRepository;
            _mailRepository = mailRepository;
        }
        public IActionResult Index(SessionState sessie) {
            if (sessie == null) {
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if (MagOefeningenBekijken(sessie)) {
                IEnumerable<Oefening> oefeningen = _oefeningRepository.GetAll().OrderBy(o => o.Graad).ThenBy(o => o.Naam).ToList();
                ViewData["Graden"] = new List<GraadEnum>(new HashSet<GraadEnum>((GraadEnum[])Enum.GetValues(typeof(GraadEnum))));
                return View(oefeningen);
            } else {
                TempData["SessionStateMessage"] = "Je moet eerst je aanwezigheid registreren.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        [HttpPost]
        public IActionResult GeefCommentaar(_CommentsViewModel commentViewModel, int id, SessionState sessie) {
            if (MagOefeningenBekijken(sessie)) {
                string comment = commentViewModel.Comments + '~' + sessie.huidigLid.Voornaam + ' ' + sessie.huidigLid.Familienaam;
                _oefeningRepository.AddComment(id, comment);
                _oefeningRepository.SaveChanges();
                bool succes = _mailRepository.SendMailAsync(comment, id).Result;
                if (succes) {
                    TempData["Message"] = "Mail succesvol verzonden.";
                } else {
                    TempData["Error"] = "Er ging iets mis bij het versturen van de mail, gelieve de lesgever te waarschuwen.";
                }
                return View("Comments", _oefeningRepository.GetById(id));
            } else {
                TempData["SessionStateMessage"] = "Je moet eerst je aanwezigheid registreren.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        [Route("Oefening/{graad}")]
        public IActionResult GeefOefeningenPerGraad(string graad, SessionState sessie) {
            TempData["Graad"] = sessie.huidigLid.GeefGraadInGetal();
            TempData["active"] = graad;
            if (MagOefeningenBekijken(sessie)) {
                if (sessie.ToegestaandOefeningenBekijken(graad, IsAlsLidIngelogd())) {
                    if (graad != "ZWART" && graad != "ALLES") {
                        return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad).OrderBy(oef => oef.Graad));
                    } else if (graad == "ZWART") {
                        return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")).OrderBy(oef => oef.Graad));
                    } else {
                        return View(nameof(Index), _oefeningRepository.GetAll().OrderBy(oef => oef.Graad));
                    }
                } else {
                    TempData["SessionStateMessage"] = "Lid beschikt niet over de juiste graad.";
                    return RedirectToAction("SessionStateMessage", "Session");
                }
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        public IActionResult GeefOefeningenLid(int lidId, SessionState sessie) {
            if (lidId <= 0) {
                TempData["SessionStateMessage"] = "LidId mag niet 0 zijn";
                return RedirectToAction("SessionStateMessage", "Session");
            }
            if(sessie == null) {
                TempData["SessionStateMessage"] = "Sessie mag niet null zijn";
                return RedirectToAction("SessionStateMessage", "Session");
            }

            var lid = _lidRepository.GetById(lidId);
            TempData["Graad"] = lid.GeefGraadInGetal();
            sessie.VeranderHuidigLid(lid);
            if (MagOefeningenBekijken(sessie)) {
                string graad = lid.Graad.ToString();
                if (!graad.StartsWith("DAN") && graad != "ALLES") {
                    return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString() == graad).OrderBy(oef => oef.Graad));
                } else if (graad.StartsWith("DAN")) {
                    return View(nameof(Index), _oefeningRepository.GetAll().Where(oef => oef.Graad.ToString().StartsWith("DAN")).OrderBy(oef => oef.Graad));
                } else {
                    return View(nameof(Index), _oefeningRepository.GetAll().OrderBy(oef => oef.Graad));
                }
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }


        public ActionResult GeefTextView(int Id, SessionState sessie) {
            if(Id <= 0 || sessie == null) {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }

            if (MagOefeningenBekijken(sessie)) {
                return View("Text", _oefeningRepository.GetById(Id));
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }
        public ActionResult GeefVideoView(int Id, SessionState sessie) {
            if (Id <= 0 || sessie == null) {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }

            if (MagOefeningenBekijken(sessie)) {
                return View("Video", _oefeningRepository.GetById(Id));
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }
        public ActionResult GeefFotoView(int Id, SessionState sessie) {
            if (Id <= 0 || sessie == null) {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }

            if (MagOefeningenBekijken(sessie)) {
                return View("Fotos", _oefeningRepository.GetById(Id));
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }
        public ActionResult GeefCommentView(int Id, SessionState sessie) {
            if (Id <= 0 || sessie == null) {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }

            if (MagOefeningenBekijken(sessie)) {
                return View("Comments", _oefeningRepository.GetById(Id));
            } else {
                TempData["SessionStateMessage"] = "Niet gemachtigd om deze oefening te bekijken.";
                return RedirectToAction("SessionStateMessage", "Session");
            }
        }

        private Lid GeefLid(int Id) {
            if (IsAlsLidIngelogd()) {
                return _lidRepository.GetByEmail(HttpContext.User.Identity.Name);
            }
            if (IsAlsLesgeverIngelogd()) {
                return _lidRepository.GetById(Id);
            }
            return null;
        }
        private bool IsAlsLesgeverIngelogd() {
            return HttpContext.User.HasClaim(c => c.Value == "lesgever");
        }
        private bool IsAlsLidIngelogd() {
            return HttpContext.User.HasClaim(c => c.Value == "lid");
        }
        private bool MagOefeningenBekijken(SessionState sessie) {
            return sessie.OefeningenBekijkenState() || IsAlsLidIngelogd();
        }
    }
}