using Application.Commands.Birds.UpdateBird;
using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Domain.Models;
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
    public class UpdateUserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private UpdateUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new UpdateUserByIdCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdUpdateUser_ReturnsUpdatedUser()
        {
            // Arrange
            var userIdToUpdate = Guid.NewGuid();
            var updateUserCommand = new UpdateUserByIdCommand(new UserDto { UserName = "NewUserName", Password = "NewPassword" }, userIdToUpdate);
            var expectedUpdatedUser = new User { Id = userIdToUpdate, UserName = "NewUserName", Password = "NewPassword" };

            _mockUserRepository.Setup(repo => repo.GetById(userIdToUpdate))
                               .ReturnsAsync(expectedUpdatedUser);

            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>()));

            // Act
            var updatedUser = await _handler.Handle(updateUserCommand, CancellationToken.None);

            // Assert
            Assert.That(updatedUser, Is.Not.Null);
            Assert.That(updatedUser.Id, Is.EqualTo(userIdToUpdate));
            Assert.That(updatedUser.UserName, Is.EqualTo(updateUserCommand.UpdatedUser.UserName));
            Assert.That(updatedUser.Password, Is.EqualTo(updateUserCommand.UpdatedUser.Password));

            _mockUserRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdUpdateUser_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateUserCommand = new UpdateUserByIdCommand(new UserDto { UserName = "NewUserName", Password = "NewPassword" }, invalidId);

            _mockUserRepository.Setup(repo => repo.GetById(invalidId)).ReturnsAsync((User)null!);
            _mockUserRepository.Setup(repo => repo.Update(It.IsAny<User>()));

            // Act
            var updatedUser = await _handler.Handle(updateUserCommand, default);

            // Assert
            Assert.That(updatedUser, Is.Null);
            _mockUserRepository.Verify(repo => repo.Update(It.IsAny<User>()), Times.Never); // Verify for extra safety. It checks so GetById is called only one time.
                                                                                            // I might have to change depending on how I want the test to work in the future.
        }
    }
}
