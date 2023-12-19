using Application.Queries.Cats.GetById;
using Application.Queries.Users.GetById;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Infrastructure.Repositories.Users;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.UserTest.QueryTests
{
    [TestFixture]
    public class GetUserByIdTests
    {
        private GetUserByIdQueryHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new GetUserByIdQueryHandler(_mockUserRepository.Object);
        }


        [Test]
        public async Task Handle_ValidId_ReturnsCorrectUser()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var query = new GetUserByIdQuery(userId);

            _mockUserRepository.Setup(repo => repo.GetById(userId)).ReturnsAsync(new User { Id = userId, UserName = "user", Password = "pass" });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));

            _mockUserRepository.Verify(repo => repo.GetById(userId), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalIdUserId = Guid.NewGuid();
            var query = new GetUserByIdQuery(invalIdUserId);

            _mockUserRepository.Setup(repo => repo.GetById(invalIdUserId)).ReturnsAsync((User)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockUserRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
