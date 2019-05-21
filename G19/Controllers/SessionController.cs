using System;
using G19.Filters;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G19.Controllers {
    [Authorize]
    [ServiceFilter(typeof(SessionFilter))]
    public class SessionController : Controller {
       
        private readonly ILidRepository _lidRepository;
      
        public SessionController(ILidRepository lidRepository) {
            _lidRepository = lidRepository;
            
        }
        [HttpGet]
        public IActionResult Index() {
            return View("Index");
        }

        [HttpGet]
        public IActionResult SessionStateMessage() {
            return View("SessionStateMessage");
        }

        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult StartNieuweSessie(SessionState sessie) {
            if (sessie == null) {
                return View(nameof(Index));
            }
            sessie.ToState(SessionEnum.RegistreerState);
            sessie.vandaag = DateTime.Today.DayOfWeek;
            return RedirectToAction("Index","Home", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
        }

        [HttpGet]
        [Authorize(Policy = "Lesgever")]
        public IActionResult FakeToday(DayOfWeek dag, SessionState sessie) {
            if (sessie == null) {
                return View(nameof(Index));
            }
            sessie.FakeVandaag(dag);
            return RedirectToAction("Index", "Home", _lidRepository.GetLedenInFormuleOfDay(sessie.vandaag));
        }

        [Authorize(Policy = "Lesgever")]
        public void EndSessionState(SessionState sessie) {
            if (sessie != null) {
                sessie.ToState(SessionEnum.EindState);
            }
        }
    }
}
