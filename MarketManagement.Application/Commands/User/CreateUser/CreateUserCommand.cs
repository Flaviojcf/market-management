using MediatR;

namespace MarketManagement.Application.Commands.User.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public CreateUserCommand(string name, string email, string password, int monthlyBudget)
        {
            Name = name;
            Email = email;
            Password = password;
            MonthlyBudget = monthlyBudget;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int MonthlyBudget { get; set; }
    }
}
