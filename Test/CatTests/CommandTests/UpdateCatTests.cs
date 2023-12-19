using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private Mock<ICatRepository> _mockCatRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCatRepository = new Mock<ICatRepository>();
            _handler = new UpdateCatByIdCommandHandler(_mockCatRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdUpdateCat_ReturnsUpdatedCat()
        {
            // Arrange
            var updateCatCommand = new UpdateCatByIdCommand(new CatDto { Name = "UpdateCat", LikesToPlay = true }, Guid.NewGuid());

            _mockCatRepository.Setup(repo => repo.GetById(updateCatCommand.Id)).ReturnsAsync(new Cat { Id = updateCatCommand.Id, Name = "ExistingCat", LikesToPlay = true });
            _mockCatRepository.Setup(repo => repo.Update(It.IsAny<Cat>()));

            // Act
            var updatedCat = await _handler.Handle(updateCatCommand, CancellationToken.None);

            // Assert
            Assert.That(updatedCat, Is.Not.Null);
            Assert.That(updatedCat.Id, Is.EqualTo(updateCatCommand.Id));
            Assert.That(updatedCat.Name, Is.EqualTo(updateCatCommand.UpdatedCat.Name));
            Assert.That(updatedCat.LikesToPlay, Is.EqualTo(updateCatCommand.UpdatedCat.LikesToPlay));

            _mockCatRepository.Verify(repo => repo.Update(It.IsAny<Cat>()), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdUpdateCat_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateCatCommand = new UpdateCatByIdCommand(new CatDto { Name = "Update", LikesToPlay = false }, invalidId);

            _mockCatRepository.Setup(repo => repo.GetById(invalidId)).ReturnsAsync((Cat)null!);
            _mockCatRepository.Setup(repo => repo.Update(It.IsAny<Cat>()));

            // Act
            var updatedCat = await _handler.Handle(updateCatCommand, default);

            // Assert
            Assert.That(updatedCat, Is.Null);
            _mockCatRepository.Verify(repo => repo.Update(It.IsAny<Cat>()), Times.Never);
        }
    }
}
