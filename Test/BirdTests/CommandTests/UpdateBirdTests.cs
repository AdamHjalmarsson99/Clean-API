using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Infrastructure.Database;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class UpdateBirdTests
    {
        private UpdateBirdByIdCommandHandler _handler;
        private GetAllBirdsQueryHandler _allBirdsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateBirdByIdCommandHandler(_mockDatabase);
            _allBirdsHandler = new GetAllBirdsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidIdUpdateBird_ReturnsUpdatedBird()
        {
            //Arrange
            var updateBirdCommand = new UpdateBirdByIdCommand(new BirdDto()
            {
                Name = "UpdateBird"
            }, new Guid("65113f59-a1c8-4c0d-9215-cf407efb2108"));

            //Act
            var updatedBird = await _handler.Handle(updateBirdCommand, CancellationToken.None);
            var updatedBirdData = await _allBirdsHandler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(updatedBird);
            Assert.That(updatedBirdData, Does.Contain(updatedBird));
        }

        [Test]
        public async Task Handle_InvalidIdUpdateBird_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateBirdCommand = new UpdateBirdByIdCommand(new BirdDto
            {
                Name = "UpdateBirdInvalid"
            }, invalidId);

            // Act
            var updatedBird = await _handler.Handle(updateBirdCommand, CancellationToken.None);

            // Assert
            Assert.Null(updatedBird);
        }
    }
}
