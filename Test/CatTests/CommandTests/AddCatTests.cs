using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Infrastructure.Database;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class AddCatTests
    {
        private AddCatCommandHandler _handler;
        private GetAllCatsQueryHandler _allCatsHandler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new MockDatabase();
            _handler = new AddCatCommandHandler(_mockDatabase);
            _allCatsHandler = new GetAllCatsQueryHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_AddNewCatValid_ReturnsCreatedCat()
        {
            //Arrange
            var addCatCommand = new AddCatCommand(new CatDto { Name = "testCat" });

            //Act
            var newAddedCat = await _handler.Handle(addCatCommand, CancellationToken.None);
            var allCats = await _allCatsHandler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            //Assert
            Assert.NotNull(newAddedCat);
            Assert.Contains(newAddedCat, allCats);
        }
    }
}
