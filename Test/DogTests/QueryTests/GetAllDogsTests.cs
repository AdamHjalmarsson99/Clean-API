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
            var specifiedBreed = "Golden Retriever";
            var specifiedWeight = 25;

            var query = new GetAllDogsQuery { Breed = specifiedBreed, Weight = specifiedWeight };
            var expectedDogs = new List<Dog>
            {
                new Dog { Id = Guid.NewGuid(), Breed = specifiedBreed, Weight = specifiedWeight, Name = "Dog1" },
                new Dog { Id = Guid.NewGuid(), Breed = specifiedBreed, Weight = specifiedWeight, Name = "Dog2" }
            };

            _mockDogRepository.Setup(x => x.GetAll(specifiedBreed, specifiedWeight)).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedDogs));
            _mockDogRepository.Verify(x => x.GetAll(specifiedBreed, specifiedWeight), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidDogList_ReturnsEmptyList()
        {

            // Arrange
            var specifiedBreed = "Rottweiler";
            var specifiedWeight = 50;
            var query = new GetAllDogsQuery { Breed = specifiedBreed, Weight = specifiedWeight };
            var expectedDogs = new List<Dog>();

            _mockDogRepository.Setup(x => x.GetAll(specifiedBreed, specifiedWeight)).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedDogs));
            _mockDogRepository.Verify(x => x.GetAll(specifiedBreed, specifiedWeight), Times.Once);
        }
    }
}
