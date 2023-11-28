using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.BirdTests.QueryTests
{
    [TestFixture]
    public class GetAllBirdsTests
    {
        private GetAllBirdsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidBirdList_ReturnsAllBirds()
        {
            //Arrange
            List<Bird> birds = _mockDatabase.Birds;

            //Act
            var result = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(birds));
        }
    }
}
