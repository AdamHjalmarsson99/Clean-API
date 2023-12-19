using Application.Dtos;
using Application.Commands.Birds.AddBird;
using Infrastructure.Repositories.Birds;
using Moq;
using Domain.Models;

namespace Test.BirdTests.CommandTests
{
    [TestFixture]
    public class AddBirdTests
    {
        private AddBirdCommandHandler _handler;
        private Mock<IBirdRepository> _mockBirdRepository;

        [SetUp]
        public void SetUp()
        {
            _mockBirdRepository = new Mock<IBirdRepository>();
            _handler = new AddBirdCommandHandler(_mockBirdRepository.Object);
        }

        [Test]
        public async Task Handle_AddNewBirdValid_ReturnsCreatedBird()
        {
            //Create a testobject for AddBirdCommand
            // Arrange
            var addBirdCommand = new AddBirdCommand(new BirdDto { Name = "testBird", CanFly = true });
            var expectedAddedBird = new Bird(); // Set your expected bird here

            _mockBirdRepository.Setup(repo => repo.Add(It.IsAny<Bird>()))
                              .ReturnsAsync(expectedAddedBird);

            // Act
            var newAddedBird = await _handler.Handle(addBirdCommand, CancellationToken.None);

            //Check so the return value isn't null
            // Assert
            Assert.That(newAddedBird, Is.Not.Null);
        }
    }
}
