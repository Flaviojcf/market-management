using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MarketManagement.Domain.Validations;

namespace MarketManagement.Domain.Services
{
    public class UserValidateService : IUserValidateService
    {
        private readonly IUserRepository _userRepository;

        public UserValidateService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ValidationResult> ValidateCreateUserAsync(string email)
        {
            var validationResult = new ValidationResult();

            if (await IsUserEmailAlreadyExists(email))
            {
                validationResult.AddError($"Email '{email}' já cadastrado.");
            }

            return validationResult;
        }

        public Task<ValidationResult> ValidateUpdateUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> IsUserEmailAlreadyExists(string email)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null) return false;

            return true;
        }
    }
}
