using MarketManagement.Domain.Entities;
using MarketManagement.Domain.Exceptions;
using MarketManagement.Domain.Repositories;
using MarketManagement.Domain.Services.Interfaces;
using MediatR;

namespace MarketManagement.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidateService _userValidateService;

        public CreateUserCommandHandler(IUserRepository userRepository, IUserValidateService userValidateService)
        {
            _userRepository = userRepository;
            _userValidateService = userValidateService;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _userValidateService.ValidateCreateUserAsync(request.Email);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"{string.Join("; ", validationResult.Errors)}");
            }

            var user = new UserEntity(request.Name, request.Email, request.Password, request.MonthlyBudget);

            await _userRepository.CreateAsync(user);

            return user.Id;
        }
    }
}
