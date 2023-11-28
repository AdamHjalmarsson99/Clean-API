using Application.Commands.Birds.DeleteBird;
using Application.Queries.Birds.GetAll;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class DeleteBirdTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private GetAllBirdsQueryHandler _allBirdsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
            _allBirdsHandler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidIdDeleteBird_ReturnsDeletedBird()
        {
            //Arrange
            var deleteBirdCommand = new DeleteBirdByIdCommand(new Guid("94d807a1-5de4-4779-9f97-dd6a7173d207"));

            //Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);
            var deletedBirddata = await _allBirdsHandler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(deletedBird);
            Assert.That(deletedBirddata, Does.Not.Contain(deletedBird));
        }

        [Test]
        public async Task Handle_InvalidIdDeleteBird_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var deleteBirdCommand = new DeleteBirdByIdCommand(invalidId);

            // Act
            var deletedBird = await _handler.Handle(deleteBirdCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedBird);
        }

    }
}
