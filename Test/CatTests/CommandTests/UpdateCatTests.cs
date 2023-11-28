using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class UpdateCatTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private GetAllCatsQueryHandler _allCatsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
            _allCatsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidIdUpdateCat_ReturnsUpdatedCat()
        {
            //Arrange
            var updateCatCommand = new UpdateCatByIdCommand(new CatDto()
            {
                Name = "UpdateCat"
            }, new Guid("e4490bed-d15e-4d80-84e7-239dd90bf587"));

            //Act
            var updatedCat = await _handler.Handle(updateCatCommand, CancellationToken.None);
            var updatedCatData = await _allCatsHandler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(updatedCat);
            Assert.That(updatedCatData, Does.Contain(updatedCat));
        }

        [Test]
        public async Task Handle_InvalidIdUpdateCat_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var updateCatCommand = new UpdateCatByIdCommand(new CatDto
            {
                Name = "UpdateCatInvalid"
            }, invalidId);

            // Act
            var updatedCat = await _handler.Handle(updateCatCommand, CancellationToken.None);

            // Assert
            Assert.Null(updatedCat);
        }
    }
}
