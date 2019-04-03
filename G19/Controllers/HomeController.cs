using Microsoft.AspNetCore.Mvc;
using G19.Models;
using G19.Models.Repositories;

namespace G19.Controllers {
    public class HomeController : Controller {
        private readonly ILidRepository _lidRepository;

        public HomeController(ILidRepository lidRepository) {
            _lidRepository = lidRepository;
        }
        public IActionResult Index() {
            return View(_lidRepository.GetAll());
        }

        //public IActionResult Privacy() {
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error() {
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
