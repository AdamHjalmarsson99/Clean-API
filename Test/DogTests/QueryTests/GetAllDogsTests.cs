using Application.Queries.Birds.GetAll;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.QueryTests
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private GetAllDogsQueryHandler _handler;
        private Mock<IDogRepository> _mockDogRepository;

        [SetUp]
        public void SetUp()
        {
            _mockDogRepository = new Mock<IDogRepository>();
            _handler = new GetAllDogsQueryHandler(_mockDogRepository.Object);
        }

        [Test]
        public async Task Handle_ValidDogList_ReturnsAllDogs()
        {
            // Arrange
            var expectedDogs = new List<Dog>
            {
                new Dog { Id = Guid.NewGuid(), Name = "Dog1"},
                new Dog { Id = Guid.NewGuid(), Name = "Dog2" }
            };

            _mockDogRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedDogs));
            _mockDogRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidDogList_ReturnsEmptyList()
        {
            // Arrange
            _mockDogRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Dog>());

            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockDogRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
