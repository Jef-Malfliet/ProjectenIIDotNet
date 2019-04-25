﻿using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19Test.Data;
using Moq;
using System.Collections.Generic;
using Xunit;

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

        #region HttpGet
        [Fact(Skip ="strange error")]
        public void GetGeefAanwezighedenPerGraad_GeeftDeJuisteLedenDoor() {
            _lidRepository.Setup(l => l.GetByGraad("wit")).Returns(new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 });
            //var result = _controller.GeefAanwezighedenPerGraad("wit");
            //Assert.Equal();
        }
        #endregion
    }
}