using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace G19Test.Controllers {
    public class HomeControllerTest {
        private readonly HomeController _controller;
        private readonly SessionState _sessie;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly DummyDbContext _context;

        public HomeControllerTest() {

            var httpcontext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpcontext, Mock.Of<ITempDataProvider>());

            _context = new DummyDbContext();
            _lidRepository = new Mock<ILidRepository>();
            _controller = new HomeController(_lidRepository.Object) {
                TempData = tempData
            };
            _sessie = new SessionState();
        }

        #region Index
        [Fact]
        public void TestIndex_Valid_GeeftIndexViewTerug() {
            _sessie.ToState(SessionEnum.RegistreerState);
            var result = _controller.Index(_sessie) as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void TestIndex_nullSessie_RedirectSessionController() {
            var result = _controller.Index(null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void TestIndex_SessieNietRegistreerState_RedirectSessionController() {
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.Index(_sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region GeefAlleLeden
        [Fact]
        public void GeefAlleLeden_Valid_GeeftIndexViewMetLeden() {
            _sessie.ToState(SessionEnum.RegistreerState);
            _lidRepository.Setup(l => l.GetAll()).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.GeefAlleLeden(_sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
            _lidRepository.Verify(l => l.GetAll(), Times.Once);
        }

        [Fact]
        public void GeefAlleLeden_NullSessie_RedirectSessionController() {
            var result = _controller.GeefAlleLeden(null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefAlleLeden_SessieNietRegistreerState_RedirectSessionController() {
            _sessie.ToState(SessionEnum.OefeningState);
            var result = _controller.GeefAlleLeden(_sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region GeefAanwezighedenPerGraad
        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_GeeftIndexTerug() {
            _lidRepository.Setup(l => l.GetByGraadEnFormuleOfDay("wit", It.IsAny<DayOfWeek>())).Returns(new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 });
            _sessie.ToState(SessionEnum.RegistreerState);
            var result = _controller.GeefAanwezighedenPerGraad("wit", _sessie) as ViewResult;
            Assert.Equal("Index", result?.ViewName);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_GeeftHetJuisteModelDoor() {
            _lidRepository.Setup(l => l.GetByGraadEnFormuleOfDay("wit", It.IsAny<DayOfWeek>())).Returns(new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 });
            _sessie.ToState(SessionEnum.RegistreerState);
            var result = _controller.GeefAanwezighedenPerGraad("wit", _sessie) as ViewResult;
            var model = new List<Lid> { _context.Lid3, _context.Lid4, _context.Lid5 };
            Assert.Equal(model, result?.Model);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_NullGraad_RedirectSessionController() {
            var result = _controller.GeefAanwezighedenPerGraad(null, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_NullSession_RedirectSessionController() {
            var result = _controller.GeefAanwezighedenPerGraad("wit", null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_NullSessionEnGraad_RedirectSessionController() {
            var result = _controller.GeefAanwezighedenPerGraad(null, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void HttpGetGeefAanwezighedenPerGraad_SessionNotRegisterState_RedirectSessionController() {
            _sessie.ToState(SessionEnum.OefeningState);
            var result = _controller.GeefAanwezighedenPerGraad("wit", _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region GeefAanwezigenVandaag
        [Fact]
        public void GeefAanwezigenVandaag_Valid_GeeftGeefAanwezigenVandaagIndexMetAanwezigeleden() {
            _lidRepository.Setup(l => l.GetAll()).Returns(new List<Lid>() { _context.Lid1 }.Where(l => l.BenIkAanwezigVandaag()));
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefAanwezigenVandaag(_sessie) as ViewResult;

            Assert.Equal("GeefAanwezigenVandaag", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }.Where(l => l.BenIkAanwezigVandaag()), result?.Model);
            _lidRepository.Verify(l => l.GetAll(), Times.Once);
        }

        [Fact]
        public void GeefAanwezigenVandaag_NullSessionEnGraad_RedirectSessionController() {
            var result = _controller.GeefAanwezigenVandaag(null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefAanwezigenVandaag_SessionNotRegisterState_RedirectSessionController() {
            _sessie.ToState(SessionEnum.RegistreerState);
            var result = _controller.GeefAanwezigenVandaag(_sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Je moet alle aanwezigen doorgeven door op de knop 'Aanwezigheden zijn geregistreerd' te drukken.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region RegistreerAanwezigheid
        [Fact]
        public void RegistreerAanwezigheid_Valid_RedirectToIndex() {
            _sessie.ToState(SessionEnum.RegistreerState);
            _lidRepository.Setup(l => l.GetById(1)).Returns(_context.Lid1);
            

            var result = _controller.RegistreerAanwezigheid(1, _sessie) as RedirectToActionResult;

            Assert.Equal("Index", result?.ActionName);
            
            _lidRepository.Verify(l => l.GetById(1), Times.Once);
            _lidRepository.Verify(l => l.SaveChanges(), Times.Once);
        }

        [Fact]
        public void RegistreerAanwezigheid_nulId_RedirectToSession() {
            var result = _controller.RegistreerAanwezigheid(-1, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void RegistreerAanwezigheid_nullSession_RedirectToSession() {
            var result = _controller.RegistreerAanwezigheid(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void RegistreerAanwezigheid_nulIdEnNullSession_RedirectToSession() {
            var result = _controller.RegistreerAanwezigheid(-1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void RegistreerAanwezigheid_SessionNotRegistreerState_RedirectToSession() {
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.RegistreerAanwezigheid(1, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region RegistreerExtraLid
        [Fact]
        public void RegistreerExtraLid_Valid_RedirectToIndexView() {
            _sessie.ToState(SessionEnum.RegistreerState);
            _lidRepository.Setup(l => l.GetByNames(It.IsAny<string>(), It.IsAny<string>())).Returns(_context.Lid1);

            var result = _controller.RegistreerExtraLid("jef", "de madafaking malfliet", _sessie) as RedirectToActionResult;

            Assert.Equal("Index", result?.ActionName);
            _lidRepository.Verify(l => l.GetByNames(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _lidRepository.Verify(l => l.RegisteerAanwezigheid(It.IsAny<Lid>()), Times.Once);
            _lidRepository.Verify(l => l.SaveChanges(), Times.Once);
        }

        [Theory]
        [InlineData("Jef", null)]
        [InlineData(null, "de enige echte Malfliet")]
        [InlineData(null, null)]
        public void RegistreerExtraLid_VoornaamEnFamilieNaamNull_RedirectToSession(string voornaam, string familienaam) {
            var result = _controller.RegistreerExtraLid(voornaam, familienaam, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Theory]
        [InlineData("Jef", null, null)]
        [InlineData(null, "de enige echte Malfliet", null)]
        [InlineData(null, null, null)]
        [InlineData("Jef", "de enige echte Malfliet", null)]
        public void RegistreerExtraLid_VoornaamEnFamilieNaamEnSessionNull_RedirectToSession(string voornaam, string familienaam, SessionState sessie) {
            var result = _controller.RegistreerExtraLid(voornaam, familienaam, sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void RegistreerExtraLid_SessionNietRegistreerState_RedirectToIndexView() {
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.RegistreerExtraLid("Jef", "is een baas", _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Alle aanwezigheden zijn reeds doorgegeven.", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region ToOefeningState
        [Fact]
        public void ToOefeningState_Valid_RedirectToGeefAanwezigenVandaag() {
            var result = _controller.ToOefeningState("1234", _sessie) as RedirectToActionResult;

            Assert.Equal("GeefAanwezigenVandaag", result?.ActionName);
            Assert.Equal(SessionEnum.OefeningState, _sessie.state);
        }

        [Fact]
        public void ToOefeningState_NullCode_RedirectToGeefAanwezigenVandaag() {
            _lidRepository.Setup(l => l.GetLedenInFormuleOfDay(It.IsAny<DayOfWeek>())).Returns(new List<Lid>() { _context.Lid1});

            var result = _controller.ToOefeningState(null, _sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
        }

        [Fact]
        public void ToOefeningState_NullSession_RedirectToGeefAanwezigenVandaag() {
            _lidRepository.Setup(l => l.GetLedenInFormuleOfDay(It.IsAny<DayOfWeek>())).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.ToOefeningState("1234", null) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
        }

        [Fact]
        public void ToOefeningState_NullSessionEnCode_RedirectToGeefAanwezigenVandaag() {
            _lidRepository.Setup(l => l.GetLedenInFormuleOfDay(It.IsAny<DayOfWeek>())).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.ToOefeningState(null, null) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
        }

        [Fact]
        public void ToOefeningState_WrongCode_RedirectToGeefAanwezigenVandaag() {
            _lidRepository.Setup(l => l.GetLedenInFormuleOfDay(It.IsAny<DayOfWeek>())).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.ToOefeningState("tjaaaaf is de baas", _sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
        }


        #endregion
    }
}
