using Application.Commands.Cats.DeleteCat;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class DeleteCatTests
    {
        private Mock<ICatRepository> _mockCatRepository;
        private DeleteCatByIdCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockCatRepository = new Mock<ICatRepository>();
            _handler = new DeleteCatByIdCommandHandler(_mockCatRepository.Object);
        }

        [Test]
        public async Task Handle_ValidIdDeleteCat_ReturnsDeletedCat()
        {
            // Arrange
            var deleteCatCommand = new DeleteCatByIdCommand(new Guid("559c67b0-7baf-45cf-980f-5d424c142b69"));
            var expectedDeletedCat = new Cat { Id = deleteCatCommand.Id, Name = "DeletedCat" };

            _mockCatRepository.Setup(repo => repo.GetById(deleteCatCommand.Id))
                               .ReturnsAsync(expectedDeletedCat);

            _mockCatRepository.Setup(repo => repo.Delete(It.IsAny<Cat>()))
                               .ReturnsAsync(expectedDeletedCat);

            // Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedCat, Is.Not.Null);
            _mockCatRepository.Verify(repo => repo.Delete(It.Is<Cat>(Cat => Cat.Id == deleteCatCommand.Id)), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidIdDeleteCat_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var deleteCatCommand = new DeleteCatByIdCommand(invalidId);

            _mockCatRepository.Setup(repo => repo.GetById(invalidId))
                               .ReturnsAsync((Cat)null!);

            // Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);

            // Assert
            Assert.That(deletedCat, Is.Null);
            _mockCatRepository.Verify(repo => repo.Delete(It.IsAny<Cat>()), Times.Never);
        }
    }
}
