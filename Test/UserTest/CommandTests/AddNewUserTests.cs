using Application.Commands.Users.AddNewUsers;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class AddNewUserTests
    {
        private AddNewUserCommandHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new AddNewUserCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_AddNewUserValid_ReturnsCreatedUser()
        {

            // Arrange
            var addUserCommand = new AddNewUserCommand(new UserDto { UserName = "testuser", Password = "pass" });
            var expectedAddedUser = new User();

            _mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>()))
                              .ReturnsAsync(expectedAddedUser);

            // Act
            var newAddedUser = await _handler.Handle(addUserCommand, CancellationToken.None);

            //Check so the return value isn't null
            // Assert
            Assert.That(newAddedUser, Is.Not.Null);
        }
    }
}
