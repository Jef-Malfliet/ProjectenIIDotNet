using G19.Controllers;
using G19.Models.Repositories;
using G19.Models.ViewModels;
using G19Test.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace G19Test.Controllers {
    public class OefeningControllerTest {

        private readonly OefeningController _controller;
        private readonly Mock<IOefeningRepository> _oefeningRepository;
        private readonly Mock<ILidRepository> _lidRepository;
        private readonly DummyDbContext _context;
        private readonly _CommentsViewModel _model;

        public OefeningControllerTest() {
            _context = new DummyDbContext();
            _oefeningRepository = new Mock<IOefeningRepository>();
            _lidRepository = new Mock<ILidRepository>();
            _controller = new OefeningController(_oefeningRepository.Object, _lidRepository.Object);
            _model = new _CommentsViewModel() {
                Comments = "Dit is de model comment" 
            };
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_VoegtCommentaarToe() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            var result = _controller.GeefCommentaar(_model, 1) as ViewResult;
            _oefeningRepository.Setup(o => o.AddComment(1, _model.Comments));
            Assert.Equal(4, _context.Oefening1.Comments.Count);
            //Assert.Equal(6, _context.Oefening1.AantalKeerBekeken);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_PersisteertGegevens() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            var result = _controller.GeefCommentaar(_model, 1) as ViewResult;
            _oefeningRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_RedirectsNaarIndex() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            var result = _controller.GeefCommentaar(_model, 1) as ViewResult;
            Assert.Equal("~/Views/Oefening/Comments.cshtml", result?.ViewName);
        }
    }
}
