using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services.Interfaces
{
    public interface IProductValidateService
    {
        Task<ValidationResult> ValidateCreateProductAsync(string name);
        Task<ValidationResult> ValidateUpdateProductAsync(Guid id);
    }
}
