using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Enums;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Tests.Domain
{
    [Collection(nameof(ProductEntity))]
    public class ProductUnitTest
    {
        [Fact]
        [Trait("Domain", "Create Product")]
        public void CreateProduct_WithValidData_ReturnsProduct()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            var product = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(Guid.Empty, product.Id);
            Assert.Equal(name, product.Name);
            Assert.Equal(currentPrice, product.CurrentPrice);
            Assert.Equal(lastMonthPrice, product.LastMonthPrice);
            Assert.Equal(categoryEnum, product.CategoryEnum);
            Assert.True(product.IsActive);
            Assert.Equal(DateTime.Now.Date, product.CreatedAt.Date);
            Assert.Equal(DateTime.MinValue, product.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Create Product")]
        public void CreateProduct_WithInvalidName_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            Action act = () => new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field name is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create Product")]
        public void CreateProduct_WithInvalidCurrentPrice_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 0;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            Action act = () => new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field currentPrice is required and must be greater than 0", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create Product")]
        public void CreateProduct_WithInvalidLastMonthPrice_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 0;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            Action act = () => new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field lastMonthPrice is required and must be greater than 0", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Create Product")]
        public void CreateProduct_WithInvalidCategoryEnum_ThrowsDomainExceptionValidation()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = (CategoryEnum)3;

            // Act
            Action act = () => new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            // Assert
            var exception = Assert.Throws<DomainException>(act);
            Assert.Contains("The field categoryEnum is required", exception.Message);
        }

        [Fact]
        [Trait("Domain", "Update Product")]
        public void UpdateProduct_WithValidData_ReturnsProduct()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            var product = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            product.Update("Arroz", 15, 10, CategoryEnum.Alimentos);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(Guid.Empty, product.Id);
            Assert.Equal("Arroz", product.Name);
            Assert.Equal(15, product.CurrentPrice);
            Assert.Equal(10, product.LastMonthPrice);
            Assert.Equal(CategoryEnum.Alimentos, product.CategoryEnum);
            Assert.True(product.IsActive);
            Assert.Equal(DateTime.Now.Date, product.CreatedAt.Date);
            Assert.Equal(DateTime.Now.Date, product.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Product")]
        public void DeActiveProduct_ReturnsDeActiveProduct()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            var product = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);

            product.Deactivate();

            // Assert
            Assert.NotNull(product);
            Assert.False(product.IsActive);
            Assert.Equal(DateTime.Now.Date, product.UpdatedAt.Date);
        }

        [Fact]
        [Trait("Domain", "Update Product")]
        public void ActiveProduct_ReturnsActiveProduct()
        {
            // Arrange
            var name = "Arroz";
            var currentPrice = 20;
            var lastMonthPrice = 15;
            var categoryEnum = CategoryEnum.Alimentos;

            // Act
            var product = new ProductEntity(name, currentPrice, lastMonthPrice, categoryEnum);
            product.Deactivate();
            product.Activate();

            // Assert
            Assert.NotNull(product);
            Assert.True(product.IsActive);
            Assert.Equal(DateTime.Now.Date, product.UpdatedAt.Date);
        }

        [Theory]
        [InlineData(null, 20, 15, CategoryEnum.Alimentos)]
        [InlineData("Feijão", null, 10, CategoryEnum.Alimentos)]
        [InlineData("Feijão", 15, null, CategoryEnum.Alimentos)]
        [InlineData("Feijão", 15, 10, (CategoryEnum)3)]
        [Trait("Domain", "Update User")]
        public void UpdateProduct_ShouldThrowDomainException_WhenInvalidDataIsProvided(string name, int currentPrice, int lastMonthPrice, CategoryEnum categoryEnum)
        {
            // Arrange
            var product = new ProductEntity("Arroz", 20, 15, CategoryEnum.Alimentos);

            // Act & Assert
            Assert.Throws<DomainException>(() => product.Update(name, currentPrice, lastMonthPrice, categoryEnum));
        }
    }
}
