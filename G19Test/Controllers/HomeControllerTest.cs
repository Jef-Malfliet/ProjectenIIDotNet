using G19.Controllers;
using G19.Models.Repositories;
using G19Test.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace G19Test.Controllers {
    public class HomeControllerTest {
        private readonly HomeController _controller;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly DummyDbContext _context;

        public HomeControllerTest() {
            _context = new DummyDbContext();
            _lidRepository = new Mock<ILidRepository>();
            _controller = new HomeController(_lidRepository.Object);
        }
    }
}
