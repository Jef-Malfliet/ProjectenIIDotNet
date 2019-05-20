using G19.Controllers;
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

        public IConfiguration Configuration { get; set; }


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

            var result = _controller.GeefCommentaar(_model, 1,_sessie) as ViewResult;

            _oefeningRepository.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GeefCommentaarPost_GeldigeCommentaar_RedirectsNaarIndex() {
            _oefeningRepository.Setup(o => o.GetById(1)).Returns(_context.Oefening1);
            _mailRepository.Setup(m => m.SendMailAsync(It.IsAny<string>(), 1)).ReturnsAsync(true);
            _sessie.ToState(SessionEnum.OefeningState);

            var result = _controller.GeefCommentaar(_model, 1,_sessie) as ViewResult;

            Assert.Equal("~/Views/Oefening/Comments.cshtml", result?.ViewName);
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
    }
}