using G19.Controllers;
using G19.Models;
using G19.Models.Repositories;
using G19.Models.ViewModels;
using G19Test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace G19Test.Controllers {
    public class LidControllerTest {

        private readonly LidController _controller;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly DummyDbContext _context;
        private readonly LidViewModel _model;
        private readonly Lid lid;

        public LidControllerTest() {
            _lidRepository = new Mock<ILidRepository>();
            _controller = new LidController(_lidRepository.Object);
            _context = new DummyDbContext();
            _model = new LidViewModel(_context.Lid1);
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
            _lidRepository.Setup(l => l.GetByEmail(It.IsAny<string>())).Returns(_context.Lid1);
            var result = _controller.Edit(_context.Lid1) as ViewResult;
            Assert.Equal(_model.ToString(), result?.Model.ToString());
        }

        [Fact]
        public void HttpGetEdit_NullUser_ReturnsNotFound() {
            _lidRepository.Setup(r => r.GetByEmail(It.IsAny<string>())).Returns((Lid)null);
            var result = _controller.Edit((Lid) null);
            Assert.IsType<NotFoundResult>(result);
        }

        //HttpPost
        [Fact]
        public void HttpPostEdit_InvalidModelState_ReturnsIndexView() {
            _controller.ModelState.AddModelError("any key", "any error");
            _lidRepository.Setup(r => r.GetByEmail(It.IsAny<string>())).Returns(_context.Lid1);
            var result = _controller.Edit(_context.Lid1, _model) as ViewResult;
            Assert.Equal("Edit", result?.ViewName);
            Assert.Equal(_model, result?.Model);
        }

        [Fact]
        public void HttpPostEdit_ValidModelState_EditsLidAndPersists() {
            _lidRepository.Setup(r => r.GetByEmail(It.IsAny<string>())).Returns(_context.Lid1);
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
            _controller.Edit(_context.Lid1,vm);
            Assert.Equal("gertjan",lid.Familienaam);
            Assert.Equal("peer", lid.Voornaam);
            _lidRepository.Verify(m => m.SaveChanges(), Times.Once);
        }
        #endregion
    }
}
