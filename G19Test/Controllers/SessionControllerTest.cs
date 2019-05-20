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
    public class SessionControllerTest {

        private readonly SessionController _controller;
        private readonly SessionState _sessie;
        private readonly Mock<ILidRepository> _lidRepostiory;
        private readonly DummyDbContext _context;

        public SessionControllerTest() {
            _lidRepostiory = new Mock<ILidRepository>();
            _context = new DummyDbContext();
            _controller = new SessionController(_lidRepostiory.Object);
            _sessie = new SessionState();
        }

        #region Index
        [Fact]
        public void TestIndex_GeeftIndexViewTerug() {
            var result = _controller.Index() as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
        #endregion

        #region Start Session
        [Fact]
        public void TestStartNieuweSessie_GeeftHomeIndexViewMetLedenVandaag() {
            _lidRepostiory.Setup(l => l.GetLedenInFormuleOfDay(DateTime.Today.DayOfWeek)).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.StartNieuweSessie(_sessie) as ViewResult;

            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
            Assert.Equal("../Home/Index", result?.ViewName);
            Assert.Equal(SessionEnum.RegistreerState, _sessie.state);
            Assert.Equal(DateTime.Today.DayOfWeek, _sessie.vandaag);
        }

        [Fact]
        public void TestStartNieuweSessie_null_KeertTerugNaarHuidigeView() {
            var result = _controller.StartNieuweSessie(null) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
        #endregion

        #region Fake Today
        [Fact]
        public void TestFakeToday_Vandaag_GeeftHomeIndexViewMetLedenMeegegevenDag() {
            _lidRepostiory.Setup(l => l.GetLedenInFormuleOfDay(DayOfWeek.Monday)).Returns(new List<Lid>() { _context.Lid1 });

            var result = _controller.FakeToday(DayOfWeek.Monday, _sessie) as ViewResult;

            Assert.Equal(DayOfWeek.Monday, _sessie.vandaag);
            Assert.Equal(new List<Lid>() { _context.Lid1 }, result?.Model);
            Assert.Equal("../Home/Index", result?.ViewName);
        }

        [Fact]
        public void TestFakeToday_null_GeeftHuidigeViewTerugSessieNull() {
            var result = _controller.FakeToday(DayOfWeek.Monday, null) as ViewResult;

            Assert.Equal("Index", result?.ViewName);
        }
        #endregion

        #region End Session State
        [Fact]
        public void TestEndSessionState_ZetSessionStateOpEindState() {
            _controller.EndSessionState(_sessie);

            Assert.Equal(SessionEnum.EindState, _sessie.state);
        }

        [Fact]
        public void TestEndSessionState_null_DoetNiks() {
            _controller.EndSessionState(null);

            Assert.NotEqual(SessionEnum.EindState, _sessie.state);
        }
        #endregion

    }
}
