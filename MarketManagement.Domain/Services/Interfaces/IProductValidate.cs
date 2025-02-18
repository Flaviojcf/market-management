using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services.Interfaces
{
    public interface IProductValidate
    {
        Task<ValidationResult> ValidateCreateProductAsync(string name);
    }
}
