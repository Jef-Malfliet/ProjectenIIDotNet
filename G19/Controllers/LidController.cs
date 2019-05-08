using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G19.Controllers {
    [Authorize]
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
        public IActionResult Edit() {
           
            Lid lid = _lidRepository.GetByEmail(HttpContext.User.Identity.Name);
            if (lid == null)
                return NotFound();
            
            return View(new LidViewModel(lid));
        }

        [HttpPost]
        public IActionResult Edit(LidViewModel lidViewModel) {
            if (ModelState.IsValid) {
                Lid lid = null;
                try {
                    lid = _lidRepository.GetByEmail(HttpContext.User.Identity.Name);

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
    }
}
