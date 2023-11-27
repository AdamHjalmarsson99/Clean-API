using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.DogTests.CommandTest
{
    public class DeleteDogTests
    {
        [TestFixture]
        public class UpdateDogTests
        {
            private DeleteDogByIdCommandHandler _handler;
            private GetAllDogsQueryHandler _allDogsHandler;
            private MockDatabase _mockDatabase;

            [SetUp]
            public void SetUp()
            {
                _mockDatabase = new MockDatabase();
                _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
                _allDogsHandler = new GetAllDogsQueryHandler(_mockDatabase);
            }

            [Test]
            public async Task Handle_ValidIdDeleteDog_ReturnsDeletedDog()
            {
                //Arrange
                var deleteDogCommand = new DeleteDogByIdCommand(new Guid("6ec82d4a-49e7-41b1-a438-845043ee7fc7"));

                //Act
                var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);
                var deletedDogdata = await _allDogsHandler.Handle(new GetAllDogsQuery(), CancellationToken.None);

                //Assert
                Assert.NotNull(deletedDog);
                Assert.That(deletedDogdata, Does.Not.Contain(deletedDog));
            }

            [Test]
            public async Task Handle_InvalidIdDeleteDog_ReturnsNull()
            {
                // Arrange
                var invalidId = Guid.NewGuid();
                var deleteDogCommand = new DeleteDogByIdCommand(invalidId);

                // Act
                var deletedDog = await _handler.Handle(deleteDogCommand, CancellationToken.None);

                // Assert
                Assert.Null(deletedDog);
            }
        }
    }
}
