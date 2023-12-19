using Application.Queries.Users.GetAll;
using Domain.Models;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.QueryTests
{
    [TestFixture]
    public class GetAllUsersTests
    {
        private GetAllUsersQueryHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new GetAllUsersQueryHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_ValidUserList_ReturnsAllUsers()
        {
            //Arrange
            var expectedUsers = new List<User>
            {
                new User {Id = Guid.NewGuid(), UserName = "user1", Password = "pass1" },
                new User {Id = Guid.NewGuid(), UserName = "user2", Password = "pass2" }
            };

            _mockUserRepository.Setup(_repo => _repo.GetAll()).ReturnsAsync(expectedUsers);

            //Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedUsers));
            _mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidUserList_ReturnsEmptyList()
        {
            // Arrange
            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User>());

            // Act
            var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockUserRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
