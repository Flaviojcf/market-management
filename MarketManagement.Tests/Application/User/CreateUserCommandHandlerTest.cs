using MarketManagement.Application.Commands.User.CreateUser;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Repositories;
using Moq;

namespace MarketManagement.Tests.Application.User
{
    [Collection(nameof(CreateUserCommand))]
    public class CreateUserCommandHandlerTest
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly CreateUserCommandHandler _createUserCommandHandler;


        public CreateUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _createUserCommandHandler = new CreateUserCommandHandler(_userRepositoryMock.Object);
        }


        [Fact]
        [Trait("Application", "Create User - Command")]
        public async Task Should_CreateUser_WhenDataIsValid()
        {
            // Arrange
            var name = "John Doe";
            var email = "johnDoe@gmail.com";
            var password = "password";
            var monthlyBudget = 1200;
            var command = new CreateUserCommand(name, email, password, monthlyBudget);

            _userRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<UserEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _createUserCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(Guid.Empty, result);
            _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>()), Times.Once);
        }
    }
}
