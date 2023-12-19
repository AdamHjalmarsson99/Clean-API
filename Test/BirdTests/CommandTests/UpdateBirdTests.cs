using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private Mock<IBirdRepository> _mockBirdRepository;

        [SetUp]
        public void SetUp()
        {
            _mockBirdRepository = new Mock<IBirdRepository>();
            _handler = new UpdateBirdByIdCommandHandler(_mockBirdRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdUpdateBird_ReturnsUpdatedBird()
        {
            // Arrange
            var updateBirdCommand = new UpdateBirdByIdCommand(new BirdDto { Name = "UpdateBird", CanFly = true }, Guid.NewGuid());

            _mockBirdRepository.Setup(repo => repo.GetById(updateBirdCommand.Id)).ReturnsAsync(new Bird { Id = updateBirdCommand.Id, Name = "ExistingBird", CanFly = true });
            _mockBirdRepository.Setup(repo => repo.Update(It.IsAny<Bird>()));

            // Act
            var updatedBird = await _handler.Handle(updateBirdCommand, CancellationToken.None);

            // Assert
            Assert.That(updatedBird, Is.Not.Null);
            Assert.That(updatedBird.Id, Is.EqualTo(updateBirdCommand.Id));
            Assert.That(updatedBird.Name, Is.EqualTo(updateBirdCommand.UpdatedBird.Name));
            Assert.That(updatedBird.CanFly, Is.EqualTo(updateBirdCommand.UpdatedBird.CanFly));

            _mockBirdRepository.Verify(repo => repo.Update(It.IsAny<Bird>()), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdUpdateBird_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateBirdCommand = new UpdateBirdByIdCommand(new BirdDto { Name = "Update", CanFly = false }, invalidId);

            _mockBirdRepository.Setup(repo => repo.GetById(invalidId)).ReturnsAsync((Bird)null!);
            _mockBirdRepository.Setup(repo => repo.Update(It.IsAny<Bird>()));

            // Act
            var updatedBird = await _handler.Handle(updateBirdCommand, default);

            // Assert
            Assert.That(updatedBird, Is.Null);
            _mockBirdRepository.Verify(repo => repo.Update(It.IsAny<Bird>()), Times.Never); //Verify for extra safety. It checks so GetById is called only one time.
                                                                                            //I might have to change depedending on how i want tyhe test to work in the future.
        }
    }
}
