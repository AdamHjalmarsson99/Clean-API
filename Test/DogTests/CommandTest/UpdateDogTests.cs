using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private Mock<IDogRepository> _mockDogRepository;

        [SetUp]
        public void SetUp()
        {
            _mockDogRepository = new Mock<IDogRepository>();
            _handler = new UpdateDogByIdCommandHandler(_mockDogRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdUpdateDog_ReturnsUpdatedDog()
        {
            // Arrange
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto { Name = "UpdateDog" }, Guid.NewGuid());

            _mockDogRepository.Setup(repo => repo.GetById(updateDogCommand.Id)).ReturnsAsync(new Dog { Id = updateDogCommand.Id, Name = "ExistingDog" });
            _mockDogRepository.Setup(repo => repo.Update(It.IsAny<Dog>()));

            // Act
            var updatedDog = await _handler.Handle(updateDogCommand, CancellationToken.None);

            // Assert
            Assert.That(updatedDog, Is.Not.Null);
            Assert.That(updatedDog.Id, Is.EqualTo(updateDogCommand.Id));
            Assert.That(updatedDog.Name, Is.EqualTo(updateDogCommand.UpdatedDog.Name));

            _mockDogRepository.Verify(repo => repo.Update(It.IsAny<Dog>()), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdUpdateDog_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto { Name = "Update" }, invalidId);

            _mockDogRepository.Setup(repo => repo.GetById(invalidId)).ReturnsAsync((Dog)null!);
            _mockDogRepository.Setup(repo => repo.Update(It.IsAny<Dog>()));

            // Act
            var updatedDog = await _handler.Handle(updateDogCommand, default);

            // Assert
            Assert.That(updatedDog, Is.Null);
            _mockDogRepository.Verify(repo => repo.Update(It.IsAny<Dog>()), Times.Never);
        }
    }
}
