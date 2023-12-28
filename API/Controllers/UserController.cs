using Application.Commands.Users.AddNewUsers;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Application.Queries.Users.GetAll;
using Application.Queries.Users.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Get all users
        [HttpGet]
        [Route("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        // Get a specific User by Id
        [HttpGet]
        [Route("getUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        //Create new user
        [HttpPost]
        [Route("addNewUser")]
        public async Task<IActionResult> AddNewUser([FromBody] CreateUserDto addNewUser)
        {
            //Validate inputdata
            var validator = new AddNewUserCommandValidator();
            var validationReslut = validator.Validate(new AddNewUserCommand(addNewUser));

            if (!validationReslut.IsValid)
            {
                return BadRequest(validationReslut.Errors.Select(error => error.ErrorMessage));
            }

            var addUserCommand = new AddNewUserCommand(addNewUser);

            try
            {
                return Ok(await _mediator.Send(new AddNewUserCommand(addNewUser)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            //if (result != null)
            //{
            //    return Ok(new { Message = "User created successfully." });
            //}

            //return BadRequest(new { Message = "Failed to create user." });
        }

        // Update a specific user by id
        [HttpPut]
        [Authorize]
        [Route("updateUser/{updatedUserId}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid updatedUserId)
        {
            var updateUserCommand = new UpdateUserByIdCommand(updatedUser, updatedUserId);
            var validationResult = await new UpdateUserByIdCommandValidator().ValidateAsync(updateUserCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var updatedUserResult = await _mediator.Send(updateUserCommand);

            if (updatedUserResult == null)
            {
                return NotFound($"User with ID {updatedUserId} not found.");
            }

            return Ok(updatedUserResult);
        }

        //Delete a specific user by id
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteUserById/{userId}")]
        public async Task<IActionResult> DeleteUserById(Guid userId)
        {
            var deletedUser = await _mediator.Send(new DeleteUserByIdCommand(userId));

            if (deletedUser == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            return Ok(deletedUser);
        }

    }
}
