using MarketManagement.Domain.Constants;
using MarketManagement.Domain.Exceptions;

namespace MarketManagement.Domain.Entities
{
    public sealed class UserEntity : BaseEntity
    {
        public UserEntity(string name, string email, string password, int monthlyBudget)
        {
            ValidateDomain(name, email, password, monthlyBudget);
            Name = name;
            Email = email;
            Password = password;
            MonthlyBudget = monthlyBudget;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public int MonthlyBudget { get; private set; }

        public void Update(string name, string email, string password, int monthlyBudget)
        {
            ValidateDomain(name, email, password, monthlyBudget);
            Name = name;
            Email = email;
            Password = password;
            MonthlyBudget = monthlyBudget;
            UpdatedAt = DateTime.Now;
        }

        private static void ValidateDomain(string name, string email, string password, int monthlyBudget)
        {
            DomainException.When(string.IsNullOrEmpty(name), string.Format(DomainMessageConstant.messageFieldIsRequired, "name"));
            DomainException.When(string.IsNullOrEmpty(email), string.Format(DomainMessageConstant.messageFieldIsRequired, "email"));
            DomainException.When(string.IsNullOrEmpty(password), string.Format(DomainMessageConstant.messageFieldIsRequired, "password"));
            DomainException.When(monthlyBudget == 0, string.Format(DomainMessageConstant.messageFieldIsRequiredAndGreaterThan, "monthlyBudget", 0));
        }
    }
}
