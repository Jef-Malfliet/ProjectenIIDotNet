using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using G19Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

[assembly: UserSecretsId("aspnet-G19-54AF2184-0909-4B6D-8E54-D1611826476F")]
namespace G19Test.Controllers {
    public class OefeningControllerTest {

        private readonly OefeningController _controller;
        private readonly Mock<IOefeningRepository> _oefeningRepository;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly Mock<IMailRepository> _mailRepository;
        private readonly DummyDbContext _context;
        private readonly _CommentsViewModel _model;
        private readonly SessionState _sessie;

        public OefeningControllerTest() {

            var httpcontext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpcontext, Mock.Of<ITempDataProvider>());

            _context = new DummyDbContext();
            _oefeningRepository = new Mock<IOefeningRepository>();
            _lidRepository = new Mock<ILidRepository>();
            _mailRepository = new Mock<IMailRepository>();
            _controller = new OefeningController(_oefeningRepository.Object, _lidRepository.Object, _mailRepository.Object) {
                TempData = tempData
            };
            _model = new _CommentsViewModel() {
                Comments = "Dit is de model comment"
            };

            _sessie = new SessionState();
            _sessie.VeranderHuidigLid(_context.Lid1);
        }

        #region Index
        [Fact]
        public void Index_valid_GeeftOefeningen() {
            _sessie.ToState(SessionEnum.OefeningState);
            _oefeningRepository.Setup(o => o.GetAll()).Returns(new List<Oefening>() { _context.Oefening1 });

            var result = _controller.Index(_sessie) as ViewResult;

            Assert.Equal(new List<Oefening>() { _context.Oefening1 }, result?.Model);
        }

        [Fact]
        public void Index_NullSessie_GeeftOefeningen() {
            var result = _controller.Index(null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
        }

        //[Fact]
        //public void Index_SessieNietOefeningState_GeeftOefeningen() {
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller.Index(_sessie) as RedirectToActionResult;

        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //}
        #endregion

        #region GeefCommentaar
        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_VoegtCommentaarToe() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1ExtraCommentaar);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefCommentaar(_model, 1, _sessie) as ViewResult;

            Assert.Equal(4, _context.Oefening1.Comments.Count);
            _oefeningRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_PersisteertGegevens() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _sessie.VeranderHuidigLid(_context.Lid1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefCommentaar(_model, 1, _sessie) as ViewResult;

            _oefeningRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_RedirectsNaarIndex() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _mailRepository.Setup(m => m.SendMailAsync(It.IsAny<string>(), 1)).ReturnsAsync(true);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefCommentaar(_model, 1, _sessie) as ViewResult;

            Assert.Equal("Comments", result?.ViewName);
            Assert.Equal(_context.Oefening1ExtraCommentaar, result?.Model);
            _oefeningRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_MailSuccesvolVerzonden() {
            _sessie.ToState(SessionEnum.OefeningState);
            _mailRepository.Setup(m => m.SendMailAsync(It.IsAny<string>(), 1)).ReturnsAsync(true);

            var result = _controller.GeefCommentaar(_model, 1, _sessie) as ViewResult;

            Assert.Equal("Mail succesvol verzonden.", _controller.TempData["Message"]);
            Assert.Null(_controller.TempData["Error"]);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_MailVerzondenMislukt() {
            _sessie.ToState(SessionEnum.OefeningState);
            _mailRepository.Setup(m => m.SendMailAsync(It.IsAny<string>(), 1)).ReturnsAsync(false);

            var result = _controller.GeefCommentaar(_model, 1, _sessie) as ViewResult;

            Assert.Null(_controller.TempData["Message"]);
            Assert.Equal("Er ging iets mis bij het versturen van de mail, gelieve de lesgever te waarschuwen.", _controller.TempData["Error"]);
        }
        #endregion

        #region GeefOefeningenPerGraad
        //[Fact]
        //public void GeefOefeningenPerGraad_validNietZwartOfAlles_GeeftIndexViewEnOefeningen() {
        //    _oefeningRepository.Setup(o => o.GetAll()).Returns(new List<Oefening>() { _context.Oefening1 });
        //    _sessie.ToState(SessionEnum.OefeningState);

        //    var result = _controller.GeefOefeningenPerGraad("Wit", _sessie) as ViewResult;

        //    Assert.Equal("Index", result?.ViewName);
        //}
        #endregion

        #region GeefOefeningenLid
        [Fact]
        public void GeefOefeningenLid_validNietZwartOfAlles_GeeftIndexViewEnOefeningen() {
            IOrderedEnumerable<Oefening> oefeningen = new List<Oefening>().OrderBy(Oef => Oef.Graad);
            _oefeningRepository.Setup(o => o.GetAll()).Returns(oefeningen);
            _context.Lid1.Graad = GraadEnum.BLAUW;
            _lidRepository.Setup(l => l.GetById(1)).Returns(_context.Lid1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefOefeningenLid(1, _sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(oefeningen, result?.Model);
            Assert.Equal(4, _controller.TempData["Graad"]);
            Assert.Equal(_context.Lid1, _sessie.huidigLid);
        }

        [Fact]
        public void GeefOefeningenLid_validZwart_GeeftIndexViewEnOefeningen() {
            IOrderedEnumerable<Oefening> oefeningen = new List<Oefening>().OrderBy(Oef => Oef.Graad);
            _oefeningRepository.Setup(o => o.GetAll()).Returns(oefeningen);
            _context.Lid1.Graad = GraadEnum.DAN9;
            _lidRepository.Setup(l => l.GetById(1)).Returns(_context.Lid1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefOefeningenLid(1, _sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(oefeningen, result?.Model);
            Assert.Equal(6, _controller.TempData["Graad"]);
            Assert.Equal(_context.Lid1, _sessie.huidigLid);
        }

        [Fact]
        public void GeefOefeningenLid_validAlles_GeeftIndexViewEnOefeningen() {
            IOrderedEnumerable<Oefening> oefeningen = new List<Oefening>().OrderBy(Oef => Oef.Graad);
            _oefeningRepository.Setup(o => o.GetAll()).Returns(oefeningen);
            _context.Lid1.Graad = GraadEnum.ALLES;
            _lidRepository.Setup(l => l.GetById(1)).Returns(_context.Lid1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefOefeningenLid(1, _sessie) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
            Assert.Equal(oefeningen, result?.Model);
            Assert.Equal(6, _controller.TempData["Graad"]);
            Assert.Equal(_context.Lid1, _sessie.huidigLid);
        }

        //[Fact]
        //public void GeefOefeningenLid_SessieNietOefeningenState_GeeftIndexViewEnOefeningen() {
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller.GeefOefeningenLid(1, _sessie) as RedirectToActionResult;

        //    Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //}

        [Fact]
        public void GeefOefeningenLid_nulId_GeeftIndexViewEnOefeningen() {
            var result = _controller.GeefOefeningenLid(-1, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("LidId mag niet kleiner zijn dan 0", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefOefeningenLid_nullSessie_GeeftIndexViewEnOefeningen() {
            var result = _controller.GeefOefeningenLid(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Sessie mag niet null zijn", _controller.TempData["SessionStateMessage"]);
        }
        #endregion

        #region GeefTextView
        [Fact]
        public void GeefTextView_Valid_GeeftTextViewEnOefeningen() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefTextView(1, _sessie) as ViewResult;

            Assert.Equal("Text", result?.ViewName);
            Assert.Equal(_context.Oefening1, result?.Model);
        }

        [Fact]
        public void GeefTextView_nulId_RedirectToSessionView() {
            var result = _controller.GeefTextView(0, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefTextView_sessieNull_RedirectToSessionView() {
            var result = _controller.GeefTextView(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        //[Fact]
        //public void GeefTextView_SessieNietOefeningenState_RedirectToSessionView() {
        //    _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller.GeefTextView(1, _sessie) as RedirectToActionResult;

        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //    Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        //}
        #endregion

        #region GeefVideoView
        [Fact]
        public void GeefVideoView_Valid_GeeftTextViewEnOefeningen() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefVideoView(1, _sessie) as ViewResult;

            Assert.Equal("Video", result?.ViewName);
            Assert.Equal(_context.Oefening1, result?.Model);
        }

        [Fact]
        public void GeefVideoView_nulId_RedirectToSessionView() {
            var result = _controller.GeefVideoView(0, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefVideoView_sessieNull_RedirectToSessionView() {
            var result = _controller.GeefVideoView(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        //[Fact]
        //public void  GeefVideoView_SessieNietOefeningenState_RedirectToSessionView() {
        //    _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller. GeefVideoView(1, _sessie) as RedirectToActionResult;

        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //    Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        //}
        #endregion

        #region GeefFotoView
        [Fact]
        public void GeefFotoView_Valid_GeeftTextViewEnOefeningen() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefFotoView(1, _sessie) as ViewResult;

            Assert.Equal("Fotos", result?.ViewName);
            Assert.Equal(_context.Oefening1, result?.Model);
        }

        [Fact]
        public void GeefFotoView_nulId_RedirectToSessionView() {
            var result = _controller.GeefFotoView(0, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefFotoView_sessieNull_RedirectToSessionView() {
            var result = _controller.GeefFotoView(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        //[Fact]
        //public void   GeefFotoView_SessieNietOefeningenState_RedirectToSessionView() {
        //    _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller.  GeefFotoView(1, _sessie) as RedirectToActionResult;

        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //    Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        //}
        #endregion

        #region GeefCommentView
        [Fact]
        public void GeefCommentView_Valid_GeeftTextViewEnOefeningen() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefCommentView(1, _sessie) as ViewResult;

            Assert.Equal("Comments", result?.ViewName);
            Assert.Equal(_context.Oefening1, result?.Model);
        }

        [Fact]
        public void GeefCommentView_nulId_RedirectToSessionView() {
            var result = _controller.GeefCommentView(0, _sessie) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        [Fact]
        public void GeefCommentView_sessieNull_RedirectToSessionView() {
            var result = _controller.GeefCommentView(1, null) as RedirectToActionResult;

            Assert.Equal("SessionStateMessage", result?.ActionName);
            Assert.Equal("Session", result?.ControllerName);
            Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        }

        //[Fact]
        //public void   GeefCommentView_SessieNietOefeningenState_RedirectToSessionView() {
        //    _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
        //    _sessie.ToState(SessionEnum.RegistreerState);

        //    var result = _controller.  GeefCommentView(1, _sessie) as RedirectToActionResult;

        //    Assert.Equal("SessionStateMessage", result?.ActionName);
        //    Assert.Equal("Session", result?.ControllerName);
        //    Assert.Equal("Niet gemachtigd om deze oefening te bekijken.", _controller.TempData["SessionStateMessage"]);
        //}
        #endregion
    }
}