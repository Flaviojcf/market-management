using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services
{
    public class ValidateCreateProduct : IProductValidate
    {
        private readonly IProductRepository _productRepository;

        public ValidateCreateProduct(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ValidationResult> ValidateCreateProductAsync(string name)
        {
            var result = new ValidationResult();

            if (await IsProductNameAlreadyRegistered(name))
            {
                result.AddError($"O produto '{name}' já foi cadastrado");
            }

            return result;
        }

        private async Task<bool> IsProductNameAlreadyRegistered(string name)
        {
            var product = await _productRepository.GetByNameAsync(name);

            if (product != null) return true;

            return false;
        }
    }
}
