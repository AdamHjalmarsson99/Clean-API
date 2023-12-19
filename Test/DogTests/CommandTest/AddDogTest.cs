using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Dogs;
using Moq;

namespace Test.DogTests.CommandTest
{
    [TestFixture]
    public class AddDogTest
    {
        private AddDogCommandHandler _handler;
        private Mock<IDogRepository> _mockDogRepository;

        [SetUp]
        public void SetUp()
        {
            _mockDogRepository = new Mock<IDogRepository>();
            _handler = new AddDogCommandHandler(_mockDogRepository.Object);
        }

        [Test]
        public async Task Handle_AddNewDogValid_ReturnsCreatedDog()
        {
            //Create a testobject for AddDogCommand
            // Arrange
            var addDogCommand = new AddDogCommand(new DogDto { Name = "testDog" });
            var expectedAddedDog = new Dog(); // Set your expected Dog here

            _mockDogRepository.Setup(repo => repo.Add(It.IsAny<Dog>()))
                              .ReturnsAsync(expectedAddedDog);

            // Act
            var newAddedDog = await _handler.Handle(addDogCommand, CancellationToken.None);

            //Check so the return value isn't null
            // Assert
            Assert.That(newAddedDog, Is.Not.Null);
        }
    }
}
