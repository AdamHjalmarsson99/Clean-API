using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;

namespace Test.BirdTests.QueryTests
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private GetAllBirdsQueryHandler _handler;
        private Mock<IBirdRepository> _mockBirdRepository;

        [SetUp]
        public void SetUp()
        {
            _mockBirdRepository = new Mock<IBirdRepository>();
            _handler = new GetAllBirdsQueryHandler(_mockBirdRepository.Object);
        }

        [Test]
        public async Task Handle_ValidBirdList_ReturnsAllBirds()
        {
            // Arrange
            var expectedColor = "Blue";

            var expectedBirds = new List<Bird>
            {
                new Bird { Id = Guid.NewGuid(), Name = "BlueBird1", Color = expectedColor, CanFly = false },
                new Bird { Id = Guid.NewGuid(), Name = "BlueBird2", Color = expectedColor, CanFly = true }
            };

            _mockBirdRepository.Setup(repo => repo.GetAll(expectedColor)).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(new GetAllBirdsQuery { Color = expectedColor }, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedBirds));
            _mockBirdRepository.Verify(repo => repo.GetAll(expectedColor), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidBirdList_ReturnsEmptyList()
        {
            // Arrange
            var specifiedColor = "Black";
            var query = new GetAllBirdsQuery { Color = specifiedColor };
            var expectedBirds = new List<Bird>();

            _mockBirdRepository.Setup(x => x.GetAll(specifiedColor)).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedBirds));
            _mockBirdRepository.Verify(x => x.GetAll(specifiedColor), Times.Once);
        }
    }
}
