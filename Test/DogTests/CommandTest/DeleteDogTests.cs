using Application.Commands.Dogs.DeleteDog;
using Domain.Models;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.CommandTest
{
    public class DeleteDogTests
    {
        [TestFixture]
        public class UpdateDogTests
        {
            private Mock<IDogRepository> _mockDogRepository;
            private DeleteDogByIdCommandHandler _handler;

            [SetUp]
            public void SetUp()
            {
                _mockDogRepository = new Mock<IDogRepository>();
                _handler = new DeleteDogByIdCommandHandler(_mockDogRepository.Object);
            }

            [Test]
            public async Task Handle_ValidIdDeleteDog_ReturnsDeletedDog()
            {
                // Arrange
                var deleteDogCommand = new DeleteDogByIdCommand(new Guid("6ec82d4a-49e7-41b1-a438-845043ee7fc7"));
                var expectedDeletedDog = new Dog { Id = deleteDogCommand.Id, Name = "DeletedDog" };

                _mockDogRepository.Setup(repo => repo.GetById(deleteDogCommand.Id))
                                   .ReturnsAsync(expectedDeletedDog);

                _mockDogRepository.Setup(repo => repo.Delete(It.IsAny<Dog>()))
                                   .ReturnsAsync(expectedDeletedDog);

                // Act
                var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

                // Assert
                Assert.That(deletedDog, Is.Not.Null);
                _mockDogRepository.Verify(repo => repo.Delete(It.Is<Dog>(dog => dog.Id == deleteDogCommand.Id)), Times.Once);
            }

            [Test]
            public async Task Handle_InvalidIdDeleteDog_ReturnsNull()
            {
                // Arrange
                var invalidId = Guid.NewGuid();
                var deleteDogCommand = new DeleteDogByIdCommand(invalidId);

                _mockDogRepository.Setup(repo => repo.GetById(invalidId))
                                   .ReturnsAsync((Dog)null!);

                // Act
                var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

                // Assert
                Assert.That(deletedDog, Is.Null);
                _mockDogRepository.Verify(repo => repo.Delete(It.IsAny<Dog>()), Times.Never);
            }
        }
    }
}
