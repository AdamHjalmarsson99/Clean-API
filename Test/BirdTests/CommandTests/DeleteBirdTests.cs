using Application.Commands.Birds.DeleteBird;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class DeleteBirdTests
    {
        private Mock<IBirdRepository> _mockBirdRepository;
        private DeleteBirdByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockBirdRepository = new Mock<IBirdRepository>();
            _handler = new DeleteBirdByIdCommandHandler(_mockBirdRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdDeleteBird_ReturnsDeletedBird()
        {
            // Arrange
            var deleteBirdCommand = new DeleteBirdByIdCommand(new Guid("94d807a1-5de4-4779-9f97-dd6a7173d207"));
            var expectedDeletedBird = new Bird { Id = deleteBirdCommand.Id, Name = "DeletedBird" };

            _mockBirdRepository.Setup(repo => repo.GetById(deleteBirdCommand.Id))
                               .ReturnsAsync(expectedDeletedBird);

            _mockBirdRepository.Setup(repo => repo.Delete(It.IsAny<Bird>()))
                               .ReturnsAsync(expectedDeletedBird);

            // Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedBird, Is.Not.Null);
            _mockBirdRepository.Verify(repo => repo.Delete(It.Is<Bird>(bird => bird.Id == deleteBirdCommand.Id)), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdDeleteBird_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var deleteBirdCommand = new DeleteBirdByIdCommand(invalidId);

            _mockBirdRepository.Setup(repo => repo.GetById(invalidId))
                               .ReturnsAsync((Bird)null!);

            // Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedBird, Is.Null);
            _mockBirdRepository.Verify(repo => repo.Delete(It.IsAny<Bird>()), Times.Never);
        }

    }
}
