using Application.Commands.Users.RemoveAnimalFromUser;
using Application.Dtos;
using Domain.Models.Animal;
using Domain.Models;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class RemoveAnimalFromUserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAnimalRepository> _mockAnimalRepository;
        private RemoveAnimalFromUserCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new RemoveAnimalFromUserCommandHandler(_mockUserRepository.Object, _mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ValidAction_RemovesAnimalFromUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var RemoveCommand = new RemoveAnimalFromUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Animals = new List<AnimalModel> { new AnimalModel { Id = animalId } } };

            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _mockAnimalRepository.Setup(x => x.GetAnimalById(animalId)).ReturnsAsync(new AnimalModel { Id = animalId });

            // Act
            var result = await _handler.Handle(RemoveCommand, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(userId));
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(animalId), Times.Once);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public async Task Handle_UserNotFound_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveAnimalFromUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });

            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync((User)null!);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(It.IsAny<Guid>()), Times.Never);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task Handle_AnimalNotFound_ReturnsUserAsItWas()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var command = new RemoveAnimalFromUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Animals = new List<AnimalModel> { new AnimalModel { Id = Guid.NewGuid() } } };

            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(It.IsAny<Guid>()), Times.Once);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }
    }
}

