using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class UpdateDogTests
    {
        private UpdateDogByIdCommandHandler _handler;
        private GetAllDogsQueryHandler _allDogsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateDogByIdCommandHandler(_mockDatabase);
            _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidIdUpdateDog_ReturnsUpdatedDog()
        {
            //Arrange
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto()
            {
                Name = "UpdateDog"
            }, new Guid("4f54036c-2faf-4c3a-b855-1db48d42f2b4"));

            //Act
            var updatedDog = await _handler.Handle(updateDogCommand, CancellationToken.None);
            var updatedDogData = await _allDogsHandler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(updatedDog);
            Assert.That(updatedDogData, Does.Contain(updatedDog));
        }

        [Test]
        public async Task Handle_InvalidIdUpdateDog_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateDogCommand = new UpdateDogByIdCommand(new DogDto
            {
                Name = "UpdateDogInvalid"
            }, invalidId);

            // Act
            var updatedDog = await _handler.Handle(updateDogCommand, CancellationToken.None);

            // Assert
            Assert.Null(updatedDog);
        }
    }
}
