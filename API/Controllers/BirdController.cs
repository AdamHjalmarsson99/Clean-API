using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public BirdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: BirdController
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds([FromQuery] string? color)
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery { Color = color }));
        }


        // Get a specific bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            //Validate inputdata
            var validator = new AddBirdCommandValidator();
            var validationReslut = validator.Validate(new AddBirdCommand(newBird));

            if (!validationReslut.IsValid)
            {
                return BadRequest(validationReslut.Errors.Select(error => error.ErrorMessage));
            }

            //If Validation succeds send command to meditor
            var createdBird = await _mediator.Send(new AddBirdCommand(newBird));

            //Return OK with createdbird
            return Ok(createdBird);
        }


        // Update a specific bird by id
        [HttpPut]
        [Authorize]
        [Route("updateBird/{updatedBirdId}")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            var updateBirdCommand = new UpdateBirdByIdCommand(updatedBird, updatedBirdId);
            var validationResult = await new UpdateBirdByIdCommandValidator().ValidateAsync(updateBirdCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var updatedBirdResult = await _mediator.Send(updateBirdCommand);

            if (updatedBirdResult == null)
            {
                return NotFound($"Bird with ID {updatedBirdId} not found.");
            }

            return Ok(updatedBirdResult);
        }

        //Delete a specific bird by id
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteBirdById/{birdId}")]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            var deletedBird = await _mediator.Send(new DeleteBirdByIdCommand(birdId));

            if (deletedBird == null)
            {
                return NotFound($"Bird with ID {birdId} not found.");
            }

            return Ok(deletedBird);
        }

    }
}
