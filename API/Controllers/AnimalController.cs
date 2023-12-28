using Application.Commands.Users.ConnectAnimalToUser;
using Application.Commands.Users.RemoveAnimalFromUser;
using Application.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnimalController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public AnimalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Route("connectAnimalToUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConnectAnimalToUser([FromBody] UserAnimalDto userToAnimal)
        {
            // Validate input data
            var validator = new ConnectAnimalToUserCommandValidator();
            var validationResult = validator.Validate(new ConnectAnimalToUserCommand(userToAnimal));

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            // Validation passed, proceed with command execution
            var connectAnimalCommand = new ConnectAnimalToUserCommand(userToAnimal);

            try
            {
                return Ok(await _mediator.Send(connectAnimalCommand));

            }
            catch (Exception ex)
            {
                // Handle exceptions as needed, log the exception, return an appropriate error response, etc.
                throw new Exception(ex.Message);
            }

        }

        [HttpDelete]
        [Route("removeAnimalFromUser")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveAnimalFromUser([FromBody] UserAnimalDto removeAnimal)
        {
            // Validate input data
            var validator = new RemoveAnimalFromUserCommandValidator();
            var validationResult = validator.Validate(new RemoveAnimalFromUserCommand(removeAnimal));

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new RemoveAnimalFromUserCommand(removeAnimal)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
