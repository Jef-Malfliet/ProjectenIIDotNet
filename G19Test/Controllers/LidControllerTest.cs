using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.State_Pattern;
using G19.Models.ViewModels;
using G19Test.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace G19Test.Controllers {
    public class LidControllerTest {

        private readonly LidController _controller;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly DummyDbContext _context;
        private readonly LidViewModel _model;
        private readonly LidViewModelSession _modelSessie;
        private readonly SessionState _sessie;
        private readonly Lid lid;

        public LidControllerTest() {
            _lidRepository = new Mock<ILidRepository>();
            _controller = new LidController(_lidRepository.Object);
            _context = new DummyDbContext();
            _model = new LidViewModel(_context.Lid1);
            _modelSessie = new LidViewModelSession(_context.Lid1);
            _sessie = new SessionState();
            lid = _context.Lid1;
        }

        #region Index
        [Fact]
        public void TestIndex_GeeftIndexViewTerug() {
            var result = _controller.Index() as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
        #endregion

        #region Edit
        //HttpGet
        [Fact]
        public void HttpGetEdit_ReturnsEditViewWithLidViewModel() {
            var result = _controller.Edit(_context.Lid1) as ViewResult;
            LidViewModel lvm = (LidViewModel)result?.Model;

            Assert.Equal(_model.Achternaam, lvm.Achternaam);
            Assert.Equal(_model.Busnummer, lvm.Busnummer);
            Assert.Equal(_model.Email, lvm.Email);
            Assert.Equal(_model.EmailOuders, lvm.EmailOuders);
            Assert.Equal(_model.GeboorteDatum, lvm.GeboorteDatum);
            Assert.Equal(_model.Geslacht, lvm.Geslacht);
            Assert.Equal(_model.Graad, lvm.Graad);
            Assert.Equal(_model.GSM, lvm.GSM);
            Assert.Equal(_model.Huisnummer, lvm.Huisnummer);
            Assert.Equal(_model.Id, lvm.Id);
            Assert.Equal(_model.Land, lvm.Land);
            Assert.Equal(_model.Lessen, lvm.Lessen);
            Assert.Equal(_model.Postcode, lvm.Postcode);
            Assert.Equal(_model.Rijksregisternummer1, lvm.Rijksregisternummer1);
            Assert.Equal(_model.Rijksregisternummer2, lvm.Rijksregisternummer2);
            Assert.Equal(_model.Rijksregisternummer3, lvm.Rijksregisternummer3);
            Assert.Equal(_model.Rijksregisternummer4, lvm.Rijksregisternummer4);
            Assert.Equal(_model.Rijksregisternummer5, lvm.Rijksregisternummer5);
            Assert.Equal(_model.Roltype, lvm.Roltype);
            Assert.Equal(_model.Stad, lvm.Stad);
            Assert.Equal(_model.StraatNaam, lvm.StraatNaam);
            Assert.Equal(_model.Telefoon, lvm.Telefoon);
            Assert.Equal(_model.Voornaam, lvm.Voornaam);
            Assert.Equal(_model.Wachtwoord, lvm.Wachtwoord);
        }

        [Fact]
        public void HttpGetEdit_NullUser_ReturnsNotFound() {
            var result = _controller.Edit((Lid)null);

            Assert.IsType<NotFoundResult>(result);
        }

        //HttpPost
        [Fact]
        public void HttpPostEdit_InvalidModelState_ReturnsIndexView() {
            _controller.ModelState.AddModelError("any key", "any error");
            
            var result = _controller.Edit(_context.Lid1, _model) as ViewResult;

            Assert.Equal("Edit", result?.ViewName);
            Assert.Equal(_model, result?.Model);
        }

        [Fact]
        public void HttpPostEdit_ValidModelState_EditsLidAndPersists() { 
            var vm = new LidViewModel() {
                Achternaam = "gertjan",
                Voornaam = "peer",
                Land = lid.Land,
                Lessen = lid.Lessen,
                Postcode = lid.PostCode,
                Busnummer = lid.Busnummer,
                Email = lid.Email,
                EmailOuders = lid.EmailOuders,
                GeboorteDatum = lid.GeboorteDatum,
                Geslacht = lid.Geslacht,
                Graad = lid.Graad,
                GSM = lid.GSM,
                Huisnummer = lid.Huisnummer,
                Rijksregisternummer1 = lid.Rijksregisternummer.Substring(0, 2),
                Rijksregisternummer2 = lid.Rijksregisternummer.Substring(3, 2),
                Rijksregisternummer3 = lid.Rijksregisternummer.Substring(6, 2),
                Rijksregisternummer4 = lid.Rijksregisternummer.Substring(9, 3),
                Rijksregisternummer5 = lid.Rijksregisternummer.Substring(13, 2),
                Roltype = lid.Roltype,
                Stad = lid.Stad,
                StraatNaam = lid.StraatNaam,
                Telefoon = lid.Telefoon,
                Wachtwoord = lid.Wachtwoord
            };

            _controller.Edit(_context.Lid1, vm);

            Assert.Equal("gertjan", lid.Familienaam);
            Assert.Equal("peer", lid.Voornaam);
            _lidRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
        #endregion

        #region EditInSession
        [Fact]
        public void HttpGetEditInSession_ReturnsEditViewWithLidViewModelSession() {
            _sessie.VeranderHuidigLid(lid);

            var result = _controller.EditInSession(_sessie) as ViewResult;
            LidViewModelSession lvm = (LidViewModelSession)result?.Model;

            Assert.Equal(_model.Busnummer, lvm.Busnummer);
            Assert.Equal(_model.Email, lvm.Email);
            Assert.Equal(_model.EmailOuders, lvm.EmailOuders);
            Assert.Equal(_model.GSM, lvm.GSM);
            Assert.Equal(_model.Huisnummer, lvm.Huisnummer);
            Assert.Equal(_model.Id, lvm.Id);
            Assert.Equal(_model.Postcode, lvm.Postcode);
            Assert.Equal(_model.Stad, lvm.Stad);
            Assert.Equal(_model.StraatNaam, lvm.StraatNaam);
            Assert.Equal(_model.Telefoon, lvm.Telefoon);
        }

        [Fact]
        public void HttpGetEditInSession_NullUser_ReturnsNotFound() {
            _sessie.VeranderHuidigLid(null);

            var result = _controller.EditInSession(_sessie);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void HttpGetEditInSession_NullSessie_ReturnsNotFound() {
            var result = _controller.EditInSession(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void HttpPostEditInSession_InvalidModelState_ReturnsIndexView() {
            _controller.ModelState.AddModelError("any key", "any error");
            _lidRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns(_context.Lid1);
            _sessie.VeranderHuidigLid(lid);

            var result = _controller.EditInSession(_modelSessie, _sessie) as ViewResult;

            Assert.Equal("EditInSession", result?.ViewName);
            Assert.Equal(_modelSessie, result?.Model);
        }

        [Fact]
        public void HttpPostEditInSession_nullSessie_ReturnsIndexView() {
            var result = _controller.EditInSession(_modelSessie, null) as ViewResult;

            Assert.Equal("EditInSession", result?.ViewName);
            Assert.Equal(_modelSessie, result?.Model);
        }

        [Fact]
        public void HttpPostEditInSession_nullModel_ReturnsIndexView() {
            var result = _controller.EditInSession(null, _sessie) as ViewResult;

            Assert.Equal("EditInSession", result?.ViewName);
            Assert.Null(result?.Model);
        }

        [Fact]
        public void HttpPostEditInSession_nullModelEnSessie_ReturnsIndexView() {
            var result = _controller.EditInSession(null, null) as ViewResult;

            Assert.Equal("EditInSession", result?.ViewName);
            Assert.Null(result?.Model);
        }

        [Fact]
        public void HttpPostEditInSession_nullSessieHuidigLid_ReturnsIndexView() {
            var result = _controller.EditInSession(null, _sessie) as ViewResult;
            _sessie.VeranderHuidigLid(null);
            Assert.Equal("EditInSession", result?.ViewName);
            Assert.Null(result?.Model);
        }

        [Fact]
        public void HttpPostEditInSession_ValidModelState_EditsLidAndPersists() {
            _sessie.VeranderHuidigLid(lid);
            _lidRepository.Setup(r => r.GetById(lid.Id)).Returns(lid);
            var vm = new LidViewModelSession() {
                Postcode = lid.PostCode,
                Busnummer = lid.Busnummer,
                Email = lid.Email,
                EmailOuders = lid.EmailOuders,
                GSM = lid.GSM,
                Huisnummer = lid.Huisnummer,
                Stad = lid.Stad,
                StraatNaam = lid.StraatNaam,
                Telefoon = lid.Telefoon,
            };

            var result = _controller.EditInSession(vm,_sessie) as RedirectToActionResult;

            Assert.Equal("Oefening", result?.ControllerName);
            Assert.Equal("GeefOefeningenLid", result?.ActionName);
            Assert.Equal("test.lid1@student.hogent.be", lid.Email);
            Assert.Equal(lid, _sessie.huidigLid);
            _lidRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
        #endregion
    }
}
