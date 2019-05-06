using G19.Controllers;
using G19.Models.Repositories;
using G19.Models.ViewModels;
using G19Test.Data;
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


        public LidControllerTest() {
            _lidRepository = new Mock<ILidRepository>();
            _controller = new LidController(_lidRepository.Object);
            _context = new DummyDbContext();
            _model = new LidViewModel(_context.Lid1);
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
            var result = _controller.Edit() as ViewResult;
            Assert.Equal(_model, result?.Model);
        }

        //HttpPost
        [Fact]
        public void HttpPostEdit() {

        }
        #endregion
    }
}
