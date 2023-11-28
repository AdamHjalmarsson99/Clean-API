using Application.Dtos;
using Infrastructure.Database;
using Application.Commands.Birds.AddBird;
using Application.Queries.Birds.GetAll;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;
        private GetAllBirdsQueryHandler _allBirdsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new AddBirdCommandHandler(_mockDatabase);
            _allBirdsHandler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewBirdValid_ReturnsCreatedBird()
        {
            //Arrange
            var addBirdCommand = new AddBirdCommand(new BirdDto { Name = "testBird" });

            //Act
            var newAddedBird = await _handler.Handle(addBirdCommand, CancellationToken.None);
            var allBirds = await _allBirdsHandler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(newAddedBird);
            Assert.Contains(newAddedBird, allBirds);
        }
    }
}
