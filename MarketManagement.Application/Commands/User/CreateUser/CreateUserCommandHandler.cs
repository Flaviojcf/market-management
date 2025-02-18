using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Repositories;
using MediatR;

namespace MarketManagement.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity(request.Name, request.Email, request.Password, request.MonthlyBudget);

            await _userRepository.CreateAsync(user);

            return user.Id;
        }
    }
}
