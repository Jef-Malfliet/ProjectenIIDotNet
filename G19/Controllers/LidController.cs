using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult EditInSession() {
            Lid lid = SessionState.huidigLid;
            if (lid == null)
                return NotFound();

            return View(new LidViewModelSession(lid));
        }

        [HttpPost]
        [Authorize(Policy = "Lid")]
        [ServiceFilter(typeof(LidFilter))]
        public IActionResult Edit(Lid lid,LidViewModel lidViewModel) {
            if (ModelState.IsValid) {
                try {
                    MapLidViewModelToLid(lidViewModel, lid);
                    _lidRepository.SaveChanges();
                }catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(Edit), lidViewModel);
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View(nameof(Edit), lidViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Lesgever")]
        public IActionResult EditInSession(LidViewModelSession lidViewModelSession) {
            Lid lid = _lidRepository.GetById(SessionState.huidigLid.Id);
            if (ModelState.IsValid) {
                try {
                    MapLidViewModelToLidInSession(lidViewModelSession, lid);
                    _lidRepository.SaveChanges();
                    SessionState.VeranderHuidigLid(lid);
                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(EditInSession), lidViewModelSession);
                }
                return RedirectToAction("GeefOefeningenLid", "Oefening", new { lidId = lid.Id });
            }

            return View(nameof(EditInSession), lidViewModelSession);
        }

        private void MapLidViewModelToLid(LidViewModel LidViewModel, Lid lid) {
                lid.Voornaam = LidViewModel.Voornaam;
                lid.Familienaam = LidViewModel.Achternaam;
                lid.Rijksregisternummer = LidViewModel.Rijksregisternummer1 + "." + LidViewModel.Rijksregisternummer2 + "."
                                     + LidViewModel.Rijksregisternummer3 + "-" + LidViewModel.Rijksregisternummer4 + "." + LidViewModel.Rijksregisternummer5;
                lid.GeboorteDatum = LidViewModel.GeboorteDatum;
                lid.Geslacht = LidViewModel.Geslacht;
                lid.Land = LidViewModel.Land;
            
           
            lid.Email = LidViewModel.Email;
            lid.GSM = LidViewModel.GSM;
            lid.Telefoon = LidViewModel.Telefoon;
            lid.Busnummer = LidViewModel.Busnummer;
            lid.Huisnummer = LidViewModel.Huisnummer;
            lid.EmailOuders = LidViewModel.EmailOuders; 
            lid.PostCode = LidViewModel.Postcode;
            lid.Stad = LidViewModel.Stad;
            lid.StraatNaam = LidViewModel.StraatNaam;

            // lid.Lessen = LidViewModel.Lessen;
            // lid.Graad = LidViewModel.Graad;
            // lid.Roltype = LidViewModel.Roltype;
            // lid.Wachtwoord = LidViewModel.Wachtwoord;
            //lid.Id = LidViewModel.Id;


        }
        private void MapLidViewModelToLidInSession(LidViewModelSession LidViewModelInSession, Lid lid) {
            lid.Email = LidViewModelInSession.Email;
            lid.GSM = LidViewModelInSession.GSM;
            lid.Telefoon = LidViewModelInSession.Telefoon;
            lid.Busnummer = LidViewModelInSession.Busnummer;
            lid.Huisnummer = LidViewModelInSession.Huisnummer;
            lid.EmailOuders = LidViewModelInSession.EmailOuders;
            lid.PostCode = LidViewModelInSession.Postcode;
            lid.Stad = LidViewModelInSession.Stad;
            lid.StraatNaam = LidViewModelInSession.StraatNaam;
            

        }
    }
}
