using Application.Queries.Cats.GetAll;
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
            //Arrange
            var expectedCats = new List<Cat>
            {
                new Cat {Id = Guid.NewGuid(), Name = "Cat1", LikesToPlay = true },
                new Cat {Id = Guid.NewGuid(), Name = "Cat2", LikesToPlay = false }
            };

            _mockCatRepository.Setup(_repo => _repo.GetAll()).ReturnsAsync(expectedCats);

            //Act
            var result = await _handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedCats));
            _mockCatRepository.Verify(repo => repo.GetAll(), Times.Once);
        }


        [Test]
        public async Task Handle_InvalidCatList_ReturnsEmptyList()
        {
            // Arrange
            _mockCatRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Cat>());

            // Act
            var result = await _handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Empty);

            _mockCatRepository.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
