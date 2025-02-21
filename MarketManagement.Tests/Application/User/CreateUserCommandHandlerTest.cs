using MarketManagement.Application.Commands.User.CreateUser;
using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;
using Moq;

namespace MarketManagement.Tests.Application.User
{
    [Collection(nameof(CreateUserCommand))]
    public class CreateUserCommandHandlerTest
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserValidateService> _userValidateServiceMock;
        private readonly CreateUserCommandHandler _createUserCommandHandler;

        public CreateUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userValidateServiceMock = new Mock<IUserValidateService>();
            _createUserCommandHandler = new CreateUserCommandHandler(_userRepositoryMock.Object, _userValidateServiceMock.Object);
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
            var validationResult = new ValidationResult();

            _userValidateServiceMock.Setup(r => r.ValidateCreateUserAsync(email)).ReturnsAsync(validationResult);

            _userRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<UserEntity>())).Returns(Task.CompletedTask);

            // Act
            var result = await _createUserCommandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Equal(Guid.Empty, result);
            _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>()), Times.Once);
        }

        [Fact]
        [Trait("Application", "Create User - Command")]
        public async Task ShouldNot_CreateUser_WhenEmailIsAlreadyRegistered()
        {
            // Arrange
            var name = "John Doe";
            var email = "johnDoe@gmail.com";
            var password = "password";
            var monthlyBudget = 1200;
            var command = new CreateUserCommand(name, email, password, monthlyBudget);
            var validationResult = new ValidationResult();

            validationResult.AddError($"Email '{email}' já cadastrado.");

            _userValidateServiceMock.Setup(r => r.ValidateCreateUserAsync(email)).ReturnsAsync(validationResult);

            // Act
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _createUserCommandHandler.Handle(command, CancellationToken.None));

            // Assert
            Assert.Equal($"Email '{email}' já cadastrado.", exception.Message);
            Assert.False(validationResult.IsValid);
            _userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>()), Times.Never);
        }
    }
}
