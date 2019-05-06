using G19.Controllers;
using G19.Models.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace G19Test.Controllers {
    public class SessionControllerTest {

        private readonly Mock<ILidRepository> _lidRepository;
        private readonly LidControllerTest _controller;

        public SessionControllerTest() {
            _lidRepository = new Mock<ILidRepository>();
            _controller = new LidController(_lidRepository.Object);
        }
    }
}
