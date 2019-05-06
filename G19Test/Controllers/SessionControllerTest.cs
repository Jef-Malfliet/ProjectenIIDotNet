using G19.Controllers;
using G19.Models.Repositories;
using G19Test.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace G19Test.Controllers {
    public class SessionControllerTest {

        private readonly SessionController _controller;
        private readonly Mock<ILidRepository> _lidRepostiory;
        private readonly DummyDbContext _context;

        public SessionControllerTest() {
            _lidRepostiory = new Mock<ILidRepository>();
            _context = new DummyDbContext();
            _controller = new SessionController(_lidRepostiory.Object);
        }

        #region Index

        #endregion

        #region Start Session

        #endregion
    }
}
