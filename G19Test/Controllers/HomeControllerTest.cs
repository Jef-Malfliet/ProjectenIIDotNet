using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19Test.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
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

        [Fact]
        public void TestIndex_GeeftIndexViewTerug() {
            SessionState.AanwezigheidRegistrerenState();
            var result = _controller.Index() as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        #region GeefAanwezighedenPerGraad
        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_GeeftIndexTerug() {
            _lidRepository.Setup(l => l.GetByGraadEnFormuleOfDay("wit", It.IsAny<DayOfWeek>())).Returns(new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 });
            SessionState.ToState(SessionEnum.RegistreerState);
            var result = _controller.GeefAanwezighedenPerGraad("wit") as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_GeeftHetJuisteModelDoor() {
            _lidRepository.Setup(l => l.GetByGraadEnFormuleOfDay("wit", It.IsAny<DayOfWeek>())).Returns(new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 });
            SessionState.ToState(SessionEnum.RegistreerState);
            var result = _controller.GeefAanwezighedenPerGraad("wit") as ViewResult;
            var model = new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 };
            Assert.Equal(model, result?.Model);
        }
        #endregion
    }
}
