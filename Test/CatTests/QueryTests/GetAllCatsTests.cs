using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.CatTests.QueryTests
{
    [TestFixture]
    public class GetAllCatsTests
    {
        private GetAllCatsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidCatList_ReturnsAllCats()
        {
            //Arrange
            List<Cat> cats = _mockDatabase.Cats;

            //Act
            var result = await _handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(cats));
        }
    }
}
