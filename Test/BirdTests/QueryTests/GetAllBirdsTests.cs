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
            var expectedBirds = new List<Bird>
            {
                new Bird { Id = Guid.NewGuid(), Name = "Bird1", CanFly = false },
                new Bird { Id = Guid.NewGuid(), Name = "Bird2", CanFly = true }
            };

            _mockBirdRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedBirds));
            _mockBirdRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidBirdList_ReturnsEmptyList()
        {
            // Arrange
            _mockBirdRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Bird>());

            // Act
            var result = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockBirdRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
