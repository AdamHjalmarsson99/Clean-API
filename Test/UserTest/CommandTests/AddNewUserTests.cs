using Application.Commands.Users.AddNewUsers;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Repositories.Users;
using Moq;

namespace Test.UserTest.CommandTests
{
    [TestFixture]
    public class AddNewUserTests
    {
        private AddNewUserCommandHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void SetUp()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new AddNewUserCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_AddNewUserValid_ReturnsCreatedUser()
        {

            //Arrange 
            var addUserCommand = new AddNewUserCommand(new CreateUserDto
            {
                UserName = "TestUser",
                Password = "Pass",
                Dogs = new List<DogDto>(),
                Cats = new List<CatDto>(),
                Birds = new List<BirdDto>()
            });

            var expectedAddedUser = new User();

            _mockUserRepository.Setup(repo => repo.Add(It.IsAny<User>()))
                              .ReturnsAsync(expectedAddedUser);

            //// Act
            var newAddedUser = await _handler.Handle(addUserCommand, CancellationToken.None);

            // Assert
            Assert.That(newAddedUser, Is.Not.Null);
            _mockUserRepository.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once());

        }
    }
}
