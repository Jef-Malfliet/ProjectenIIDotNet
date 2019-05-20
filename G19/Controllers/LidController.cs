using System;
using System.Collections.Generic;
using G19.Filters;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G19.Controllers {
    [Authorize]
    [ServiceFilter(typeof(LidFilter))]
    [ServiceFilter(typeof(SessionFilter))]
    public class LidController : Controller {
        // GET: /<controller>/
        private readonly ILidRepository _lidRepository;
        public LidController(ILidRepository lidRepository) {
            _lidRepository = lidRepository;

        }
        [Authorize(Policy = "Lid")]
        public IActionResult Index() {
            return View("Index");
        }

        [HttpGet]
        [Authorize(Policy = "Lid")]
        [ServiceFilter(typeof(LidFilter))]
        public IActionResult Edit(Lid lid) {

            if (lid == null)
                return NotFound();

            return View(new LidViewModel(lid));
        }

        [HttpPost]
        [Authorize(Policy = "Lid")]
        [ServiceFilter(typeof(LidFilter))]
        public IActionResult Edit(Lid lid, LidViewModel lidViewModel) {
            if (ModelState.IsValid) {
                try {
                    lid.MapLidViewModelToLid(lidViewModel, lid);
                    _lidRepository.SaveChanges();
                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(Edit), lidViewModel);
                }
                return RedirectToAction(nameof(Index), "Session");
            }

            return View(nameof(Edit), lidViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult EditInSession(SessionState sessie) {
            if(sessie == null) {
                return NotFound();
            }
            Lid lid = sessie.huidigLid;
            if (lid == null)
                return NotFound();

            return View(new LidViewModelSession(lid));
        }

        [HttpPost]
        [Authorize(Policy = "Lesgever")]
        public IActionResult EditInSession(LidViewModelSession lidViewModelSession, SessionState sessie) {
            if(lidViewModelSession == null) {
                return View(nameof(EditInSession), lidViewModelSession);
            }
            if(sessie == null) {
                return View(nameof(EditInSession), lidViewModelSession);
            }
            if(sessie.huidigLid == null) {
                return View(nameof(EditInSession), lidViewModelSession);
            }

            Lid lid = _lidRepository.GetById(sessie.huidigLid.Id);
            if (ModelState.IsValid) {
                try {
                    lid.MapLidViewModelToLidInSession(lidViewModelSession, lid);
                    _lidRepository.SaveChanges();
                    sessie.VeranderHuidigLid(lid);
                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(EditInSession), lidViewModelSession);
                }
                return RedirectToAction("GeefOefeningenLid", "Oefening", new { lidId = lid.Id });
            }

            return View(nameof(EditInSession), lidViewModelSession);
        }

        [HttpGet]
        public IActionResult RegistreerNietLid() {
            TempData["isNietLid"] = "true";
            return View("Edit");
        }

        [HttpPost]
        public IActionResult RegistreerNietLid(LidViewModel nietLidVM, SessionState sessie) {
            if (ModelState.IsValid) {
                try {
                    Lid nietLid = new Lid() { Roltype = RolTypeEnum.Niet_lid, Wachtwoord = "NietLidWachtwoord", Graad = GraadEnum.WIT };

                    nietLid.MapLidViewModelToLid(nietLidVM, nietLid);
                    _lidRepository.Add(nietLid);
                    _lidRepository.SaveChanges();
                    List<Lid_Aanwezigheden> aanw = new List<Lid_Aanwezigheden>();
                    aanw.Add(new Lid_Aanwezigheden() { LidId = nietLid.Id, Aanwezigheid = DateTime.Now });
                    nietLid.Aanwezigheden = aanw;
                    _lidRepository.SaveChanges();

                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(Edit), nietLidVM);
                }
                return View("~/Views/Home/Index.cshtml", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
            }
            return View(nameof(Edit), nietLidVM);
        }
    }
}
