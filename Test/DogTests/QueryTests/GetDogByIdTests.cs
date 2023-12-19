using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.QueryTest
{
    [TestFixture]
    public class GetDogByIdTests
    {
        private GetDogByIdQueryHandler _handler;
        private Mock<IDogRepository> _mockDogRepository;

        [SetUp]
        public void SetUp()
        {
            _mockDogRepository = new Mock<IDogRepository>();
            _handler = new GetDogByIdQueryHandler(_mockDogRepository.Object);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectDog()
        {
            // Arrange
            var dogId = new Guid("12345678-1234-5678-1234-567812345678");
            var query = new GetDogByIdQuery(dogId);

            _mockDogRepository.Setup(repo => repo.GetById(dogId)).ReturnsAsync(new Dog { Id = dogId, Name = "MockedDog" });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(dogId));

            _mockDogRepository.Verify(repo => repo.GetById(dogId), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidDogId = Guid.NewGuid();
            var query = new GetDogByIdQuery(invalidDogId);

            _mockDogRepository.Setup(repo => repo.GetById(invalidDogId)).ReturnsAsync((Dog)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockDogRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
