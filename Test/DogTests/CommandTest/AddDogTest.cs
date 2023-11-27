using Application.Commands.Dogs;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTest
    {
        private AddDogCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new AddDogCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewDogValid_ReturnsCreatedDog()
        {
            //Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "testDog" });

            //Act
            var newAddedDog = await _handler.Handle(addDogCommand, CancellationToken.None);
            var allDogs = await _allDogsHandler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(newAddedDog);
            Assert.Contains(newAddedDog, allDogs);
        }
    }
}
