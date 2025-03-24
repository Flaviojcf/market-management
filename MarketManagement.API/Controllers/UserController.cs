using MarketManagement.Application.Commands.User.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MarketManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateUserAccount([FromBody] CreateUserCommand createUserCommand)
        {
            var loginRecord = await _mediator.Send(createUserCommand);

            return Ok(loginRecord);
        }
    }
}
