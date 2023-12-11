using Application.Commands.Cats.UpdateCat;
using Application.Commands.Dogs;
using Application.Commands.Dogs.AddDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public DogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
        }

        // Get a specific dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        // Create a new dog 
        [HttpPost]
        [Authorize]
        [Route("addNewDog")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            //Validate inputdata
            var validator = new AddDogCommandValidator();
            var validationReslut = validator.Validate(new AddDogCommand(newDog));

            if (!validationReslut.IsValid)
            {
                return BadRequest(validationReslut.Errors.Select(error => error.ErrorMessage));
            }

            //If Validation succeds send command to meditor
            var createdDog = await _mediator.Send(new AddDogCommand(newDog));

            //Return OK with createdbird
            return Ok(createdDog);
        }

        // Update a specific dog by id
        [HttpPut]
        [Authorize]
        [Route("updateDog/{updatedDogId}")]
        [ProducesResponseType(typeof(Dog), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            var updateDogCommand = new UpdateDogByIdCommand(updatedDog, updatedDogId);
            var validationResult = await new UpdateDogByIdCommandValidator().ValidateAsync(updateDogCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var updatedDogResult = await _mediator.Send(updateDogCommand);

            if (updatedDogResult == null)
            {
                return NotFound($"Dog with ID {updatedDogId} not found.");
            }

            return Ok(updatedDogResult);
        }

        //Delete a specific dog by id
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteDogById/{dogId}")]
        public async Task<IActionResult> DeleteDogById(Guid dogId)
        {
            var deletedDog = await _mediator.Send(new DeleteDogByIdCommand(dogId));

            if (deletedDog == null)
            {
                return NotFound($"Dog with ID {dogId} not found.");
            }

            return Ok(deletedDog);
        }
    }
}
