using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Database;

namespace Test.DogTests.QueryTests
{
    [TestFixture]
    public class GetAllDogsTests
    {
        private GetAllDogsQueryHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidDogList_ReturnsAllDogs()
        {
            //Arrange
            List<Dog> dogs = _mockDatabase.Dogs;

            //Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.That(result, Is.EqualTo(dogs));
        }
    }
}
