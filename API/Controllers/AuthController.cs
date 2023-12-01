using Application.Commands.Birds.DeleteBird;
using Application.Commands.Users.AddNewUsers;
using Application.Commands.Users.LogInUser;
using Application.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        //Create new user
        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody] UserDto addNewUser)
        {
            var addUserCommand = new AddNewUserCommand(addNewUser);

            var result = await _mediator.Send(addUserCommand);

            if (result != null)
            {
                return Ok(new { Message = "User created successfully." });
            }

            return BadRequest(new { Message = "Failed to create user." });
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
