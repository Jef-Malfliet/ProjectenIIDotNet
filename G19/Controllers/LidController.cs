using System;
using System.Collections;
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

        public IActionResult Index() {
            return View("Index");
        }

        [HttpGet]
        [ServiceFilter(typeof(LidFilter))]
        public IActionResult Edit(Lid lid) {

            if (lid == null)
                return NotFound();

            return View(new LidViewModel(lid));
        }

        [HttpPost]
        [ServiceFilter(typeof(LidFilter))]
        public IActionResult Edit(Lid lid, LidViewModel lidViewModel) {
            if (ModelState.IsValid) {
                try {
                    MapLidViewModelToLid(lidViewModel, lid);
                    _lidRepository.SaveChanges();
                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(Edit), lidViewModel);
                }
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Edit), lidViewModel);
        }

        private void MapLidViewModelToLid(LidViewModel LidViewModel, Lid lid) {
            lid.Voornaam = LidViewModel.Voornaam;
            lid.Familienaam = LidViewModel.Achternaam;
            lid.Email = LidViewModel.Email;
            lid.GSM = LidViewModel.GSM;
            lid.Telefoon = LidViewModel.Telefoon;
            lid.Rijksregisternummer = LidViewModel.Rijksregisternummer1 + "." + LidViewModel.Rijksregisternummer2 + "."
                                      + LidViewModel.Rijksregisternummer3 + "-" + LidViewModel.Rijksregisternummer4 + "." + LidViewModel.Rijksregisternummer5;
            lid.Busnummer = LidViewModel.Busnummer;
            lid.Huisnummer = LidViewModel.Huisnummer;
            lid.EmailOuders = LidViewModel.EmailOuders;
            lid.GeboorteDatum = LidViewModel.GeboorteDatum;
            lid.Geslacht = LidViewModel.Geslacht;
            lid.Land = LidViewModel.Land;
            lid.PostCode = LidViewModel.Postcode;
            lid.Stad = LidViewModel.Stad;
            lid.StraatNaam = LidViewModel.StraatNaam;
            // lid.Lessen = LidViewModel.Lessen;
            // lid.Graad = LidViewModel.Graad;
            // lid.Roltype = LidViewModel.Roltype;
            // lid.Wachtwoord = LidViewModel.Wachtwoord;
            //lid.Id = LidViewModel.Id;


        }

        [HttpGet]
        public IActionResult RegistreerNietLid() {
            TempData["isNietLid"] = "true";
            return View("Edit");
        }

        [HttpPost]
        public IActionResult RegistreerNietLid(LidViewModel nietLidVM) {
            if (ModelState.IsValid) {
                try {
                    Lid nietLid = new Lid() { Roltype = RolTypeEnum.Niet_lid, Wachtwoord = "NietLidWachtwoord", Graad = GraadEnum.WIT };
                    MapLidViewModelToLid(nietLidVM, nietLid);
                    _lidRepository.Add(nietLid);
                    _lidRepository.SaveChanges();
                } catch (Exception e) {
                    ModelState.AddModelError("", e.Message);
                    return View(nameof(Edit), nietLidVM);
                }
                return View("~/Views/Home/Index.cshtml", _lidRepository.GetLedenInFormuleOfDay(SessionState.vandaag));
            }
            return View(nameof(Edit), nietLidVM);
        }
    }
}
