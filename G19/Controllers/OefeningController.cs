using G19.Models;
using G19.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
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
            return View(oefeningen);
        }

        public IActionResult geefCommentaar(int id) {
            return null;
        }
    }
}