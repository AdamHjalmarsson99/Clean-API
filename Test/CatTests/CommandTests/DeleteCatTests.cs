using Application.Commands.Cats.DeleteCat;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class DeleteCatTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private GetAllCatsQueryHandler _allCatsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
            _allCatsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_ValidIdDeleteCat_ReturnsDeletedCat()
        {
            //Arrange
            var deleteCatCommand = new DeleteCatByIdCommand(new Guid("559c67b0-7baf-45cf-980f-5d424c142b69"));

            //Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);
            var deletedCatdata = await _allCatsHandler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(deletedCat);
            Assert.That(deletedCatdata, Does.Not.Contain(deletedCat));
        }

        [Test]
        public async Task Handle_InvalidIdDeleteCat_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();
            var deleteCatCommand = new DeleteCatByIdCommand(invalidId);

            // Act
            var deletedCat = await _handler.Handle(deleteCatCommand, CancellationToken.None);

            // Assert
            Assert.Null(deletedCat);
        }
    }
}
