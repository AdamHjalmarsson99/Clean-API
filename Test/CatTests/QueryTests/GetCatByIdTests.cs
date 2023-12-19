using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.QueryTests
{
    [TestFixture]
    public class GetCatByIdTests
    {
        private GetCatByIdQueryHandler _handler;
        private Mock<ICatRepository> _mockCatRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCatRepository = new Mock<ICatRepository>();
            _handler = new GetCatByIdQueryHandler(_mockCatRepository.Object);
        }

        [Test]
        public async Task Handle_ValidId_ReturnsCorrectCat()
        {
            //Arrange
            var catId = new Guid("bf49c5e3-e438-42e7-8f2f-4a6d3656757d");
            var query = new GetCatByIdQuery(catId);

            _mockCatRepository.Setup(repo => repo.GetById(catId)).ReturnsAsync(new Cat { Id = catId, Name = "MockedCat", LikesToPlay = true });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(catId));

            _mockCatRepository.Verify(repo => repo.GetById(catId), Times.Once);
        }

        [Test]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalIdCatId = Guid.NewGuid();
            var query = new GetCatByIdQuery(invalIdCatId);

            _mockCatRepository.Setup(repo => repo.GetById(invalIdCatId)).ReturnsAsync((Cat)null!);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
            _mockCatRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
