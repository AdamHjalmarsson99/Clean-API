using Application.Queries.Cats.GetAll;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.QueryTests
{
    [TestFixture]
    public class GetAllCatsTests
    {
        private GetAllCatsQueryHandler _handler;
        private Mock<ICatRepository> _mockCatRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCatRepository = new Mock<ICatRepository>();
            _handler = new GetAllCatsQueryHandler(_mockCatRepository.Object);
        }

        [Test]
        public async Task Handle_ValidCatList_ReturnsAllCats()
        {
            var specifiedBreed = "Maine Coon";
            var specifiedWeight = 14;

            var query = new GetAllCatsQuery { Breed = specifiedBreed, Weight = specifiedWeight };
            var expectedCats = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Breed = specifiedBreed, Weight = specifiedWeight, Name = "Cat1" },
                new Cat { Id = Guid.NewGuid(), Breed = specifiedBreed, Weight = specifiedWeight, Name = "Cat2" }
            };

            _mockCatRepository.Setup(x => x.GetAll(specifiedBreed, specifiedWeight)).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedCats));
            _mockCatRepository.Verify(x => x.GetAll(specifiedBreed, specifiedWeight), Times.Once);
        }


        [Test]
        public async Task Handle_InvalidCatList_ReturnsEmptyList()
        {
            // Arrange
            var specifiedBreed = "Bengal";
            var specifiedWeight = 7;
            var query = new GetAllCatsQuery { Breed = specifiedBreed, Weight = specifiedWeight };
            var expectedCats = new List<Cat>();

            _mockCatRepository.Setup(x => x.GetAll(specifiedBreed, specifiedWeight)).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);
            Assert.That(result, Is.EqualTo(expectedCats));
            _mockCatRepository.Verify(x => x.GetAll(specifiedBreed, specifiedWeight), Times.Once);
        }
    }
}
