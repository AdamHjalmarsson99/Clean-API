using Application.Commands.Users.LogInUser;
using Application.Dtos;
using Infrastructure.Repositories.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class LogInUserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private LogInUserCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new LogInUserCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_LogInIsValid_ReturnsToken()
        {
            // Arrange
            var loginUserCommand = new LogInUserCommand(new UserDto { UserName = "user", Password = "password" });
            var expectedToken = "Token";

            _mockUserRepository.Setup(repo => repo.LogIn(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedToken);

            // Act
            var result = await _handler.Handle(loginUserCommand, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedToken));
            _mockUserRepository.Verify(repo => repo.LogIn(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
