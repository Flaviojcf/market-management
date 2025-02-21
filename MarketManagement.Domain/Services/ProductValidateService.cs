using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services
{
    public class ProductValidateService : IProductValidateService
    {
        private readonly IProductRepository _productRepository;

        public ProductValidateService(IProductRepository productRepository)
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

        public async Task<ValidationResult> ValidateUpdateProductAsync(Guid id)
        {
            var result = new ValidationResult();

            if (!await IsProductExists(id))
            {
                result.AddError($"O produto '{id}' não existe.");
            }

            return result;
        }

        private async Task<bool> IsProductNameAlreadyRegistered(string name)
        {
            var product = await _productRepository.GetByNameAsync(name);

            if (product != null) return true;

            return false;
        }

        private async Task<bool> IsProductExists(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product != null) return true;

            return false;
        }
    }
}
