using Application.Commands.Users.LogInUser;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<IActionResult> UserLogIn([FromBody] UserDto userLogIn)
        {
            var logInUserCommand = new LogInUserCommand(userLogIn);
            var logInUserCommandReslut = await _mediator.Send(logInUserCommand);

            if (logInUserCommandReslut == null)
                return NotFound("Incorrect Password or Username");

            return Ok(logInUserCommandReslut);
        }

    }

}
