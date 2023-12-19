using Application.Queries.Birds.GetById;
using Domain.Models;
using Infrastructure.Repositories.Birds;
using Moq;

namespace Test.BirdTests.QueryTests
{
    [TestFixture]
    public class GetBirdByIdTests
    {
        private GetBirdByIdQueryHandler _handler;
        private Mock<IBirdRepository> _mockBirdRepository;

        [SetUp]
        public void SetUp()
        {
            _mockBirdRepository = new Mock<IBirdRepository>();
            _handler = new GetBirdByIdQueryHandler(_mockBirdRepository.Object);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectBird()
        {
            //Arrange
            var birdId = new Guid("60fdbc14-3f6c-4ddb-90a5-89c2e465be12");
            var query = new GetBirdByIdQuery(birdId);

            _mockBirdRepository.Setup(repo => repo.GetById(birdId)).ReturnsAsync(new Bird { Id = birdId, Name = "MockedBird", CanFly = true });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(birdId));

            _mockBirdRepository.Verify(repo => repo.GetById(birdId), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalIdBirdId = Guid.NewGuid();
            var query = new GetBirdByIdQuery(invalIdBirdId);

            _mockBirdRepository.Setup(repo => repo.GetById(invalIdBirdId)).ReturnsAsync((Bird)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockBirdRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }

    }
}
