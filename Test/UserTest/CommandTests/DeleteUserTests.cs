using Application.Commands.Users.DeleteUser;
using Domain.Models;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class DeleteUserByIdTest
    {
        private Mock<IUserRepository> _mockUserRepository;
        private DeleteUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new DeleteUserByIdCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdDeleteUser_ReturnsDeletedUser()
        {
            // Arrange
            var userIdToDelete = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserByIdCommand(userIdToDelete);
            var expectedDeletedUser = new User { Id = userIdToDelete, UserName = "DeletedUser" };

            _mockUserRepository.Setup(repo => repo.GetById(userIdToDelete))
                               .ReturnsAsync(expectedDeletedUser);

            _mockUserRepository.Setup(repo => repo.Delete(It.IsAny<User>()))
                               .ReturnsAsync(expectedDeletedUser);

            // Act
            var deletedUser = await _handler.Handle(deleteUserCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedUser, Is.Not.Null);
            _mockUserRepository.Verify(repo => repo.Delete(It.Is<User>(user => user.Id == userIdToDelete)), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdDeleteUser_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserByIdCommand(invalidId);

            _mockUserRepository.Setup(repo => repo.GetById(invalidId))
                               .ReturnsAsync((User)null!);

            // Act
            var deletedUser = await _handler.Handle(deleteUserCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedUser, Is.Null);
            _mockUserRepository.Verify(repo => repo.Delete(It.IsAny<User>()), Times.Never);
        }
    }
}
