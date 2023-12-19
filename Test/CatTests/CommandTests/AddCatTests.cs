using Application.Commands.Cats.AddCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Cats;
using Moq;

namespace Test.CatTests.CommandTests
{
    [TestFixture]
    public class AddCatTests
    {
        private AddCatCommandHandler _handler;
        private Mock<ICatRepository> _mockCatRepository;

        [SetUp]
        public void SetUp()
        {
            _mockCatRepository = new Mock<ICatRepository>();
            _handler = new AddCatCommandHandler(_mockCatRepository.Object);
        }

        [Test]
        public async Task Handle_AddNewCatValid_ReturnsCreatedCat()
        {
            //Create a testobject for AddCatCommand
            // Arrange
            var addCatCommand = new AddCatCommand(new CatDto { Name = "testCat", LikesToPlay = true });
            var expectedAddedCat = new Cat(); // Set your expected Cat here

            _mockCatRepository.Setup(repo => repo.Add(It.IsAny<Cat>()))
                              .ReturnsAsync(expectedAddedCat);

            // Act
            var newAddedCat = await _handler.Handle(addCatCommand, CancellationToken.None);

            //Check so the return value isn't null
            // Assert
            Assert.That(newAddedCat, Is.Not.Null);
        }
    }
}
