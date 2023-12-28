using Application.Commands.Users.ConnectAnimalToUser;
using Application.Dtos;
using Domain.Models;
using Domain.Models.Animal;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class ConnectAnimalToUserTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAnimalRepository> _mockAnimalRepository;
        private ConnectAnimalToUserCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new ConnectAnimalToUserCommandHandler(_mockUserRepository.Object, _mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ValidAction_ReturnsUpdatedUserData()
        {

            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var connectionCommand = new ConnectAnimalToUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Animals = new List<AnimalModel>() };
            var existingAnimal = new AnimalModel { Id = animalId };

            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _mockAnimalRepository.Setup(x => x.GetAnimalById(animalId)).ReturnsAsync(existingAnimal);

            // Act
            var result = await _handler.Handle(connectionCommand, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(userId, Is.EqualTo(result.Id));
            Assert.IsTrue(result.Animals.Contains(existingAnimal));
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(animalId), Times.Once);
            _mockUserRepository.Verify(x => x.Update(existingUser), Times.Once);
        }

        [Test]
        public async Task Handle_UserIsNull_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var connectionCommand = new ConnectAnimalToUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });


            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync((User)null!);

            // Act
            var result = await _handler.Handle(connectionCommand, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(It.IsAny<Guid>()), Times.Never);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }

        [Test]
        public async Task Handle_AnimalIsNull_ReturnsNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();
            var connectionCommand = new ConnectAnimalToUserCommand(new UserAnimalDto { UserId = userId, AnimalId = animalId });

            var existingUser = new User { Id = userId, Animals = new List<AnimalModel>() };

            _mockUserRepository.Setup(x => x.GetById(userId)).ReturnsAsync(existingUser);
            _mockAnimalRepository.Setup(x => x.GetAnimalById(animalId)).Returns(Task.FromResult((AnimalModel?)null!));

            // Act
            var result = await _handler.Handle(connectionCommand, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockUserRepository.Verify(x => x.GetById(userId), Times.Once);
            _mockAnimalRepository.Verify(x => x.GetAnimalById(animalId), Times.Once);
            _mockUserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never);
        }
    }
}
