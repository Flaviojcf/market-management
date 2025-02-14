using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Tests.Domain
{
    [Collection(nameof(UserEntity))]
    public class UserUnitTest
    {
        [Fact]
        [Trait("Domain", "Create User")]
        public void CreateUser_WithValidData_ReturnsUser()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(Guid.Empty, user.Id);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(monthlyBudget, user.MonthlyBudget);
            Assert.Equal(password, user.Password);
            Assert.True(user.IsActive);
            Assert.Equal(DateTime.Now.Date, user.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, user.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Create User")]
        public void CreateUser_WithInvalidName_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            Action act = () => new UserEntity(name, email, password, monthlyBudget);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field name is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create User")]
        public void CreateUser_WithInvalidEmail_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            Action act = () => new UserEntity(name, email, password, monthlyBudget);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field email is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create User")]
        public void CreateUser_WithInvalidPassword_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "";
            var monthlyBudget = 1000;

            // Act
            Action act = () => new UserEntity(name, email, password, monthlyBudget);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field password is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create User")]
        public void CreateUser_WithInvalidMontlhlyBudget_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 0;

            // Act
            Action act = () => new UserEntity(name, email, password, monthlyBudget);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field monthlyBudget is required and must be greater than 0", exception.Message);
        }


        [Fact]
        [Trait("Domain", "Update User")]
        public void UpdateUser_WithValidData_ReturnsUser()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);

            user.Update("John Doe2", "john.doe2@example.com", "password2", 2000);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(Guid.Empty, user.Id);
            Assert.Equal("John Doe2", user.Name);
            Assert.Equal("john.doe2@example.com", user.Email);
            Assert.Equal("password2", user.Password);
            Assert.Equal(2000, user.MonthlyBudget);
            Assert.True(user.IsActive);
            Assert.Equal(DateTime.Now.Date, user.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, user.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update User")]
        public void UpdateUser_WithInvalidName_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);
            var exception = Assert.Throws<DomainException>(() => user.Update("", "john.doe2@example.com", "p@ssw0rd2", 2000));


            // Assert
            Assert.Contains("The field name is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Update User")]
        public void UpdateUser_WithInvalidEmail_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);
            var exception = Assert.Throws<DomainException>(() => user.Update("John Doe", "", "p@ssw0rd2", 2000));


            // Assert
            Assert.Contains("The field email is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Update User")]
        public void UpdateUser_WithInvalidPassword_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);
            var exception = Assert.Throws<DomainException>(() => user.Update("John Doe", "john.doe@example.com", "", 2000));

            // Assert
            Assert.Contains("The field password is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Update User")]
        public void UpdateUser_WithInvalidMonthlyBudget_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "John Doe";
            var email = "john.doe@example.com";
            var password = "p@ssw0rd";
            var monthlyBudget = 1000;

            // Act
            var user = new UserEntity(name, email, password, monthlyBudget);
            var exception = Assert.Throws<DomainException>(() => user.Update("John Doe", "john.doe@example.com", "p@ssw0rd", 0));

            // Assert
            Assert.Contains("The field monthlyBudget is required and must be greater than 0", exception.Message);
        }
    }
}