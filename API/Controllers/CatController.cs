using Application.Commands.Cats.AddCat;
using Application.Commands.Cats.DeleteCat;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public CatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats([FromQuery] string? breed, [FromQuery] int? weight)
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery { Breed = breed, Weight = weight }));
        }


        // Get a specific cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat 
        [HttpPost]
        //[Authorize]
        [Route("addNewCat")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            //Validate inputdata
            var validator = new AddCatCommandValidator();
            var validationReslut = validator.Validate(new AddCatCommand(newCat));

            if (!validationReslut.IsValid)
            {
                return BadRequest(validationReslut.Errors.Select(error => error.ErrorMessage));
            }

            //If Validation succeds send command to meditor
            var createdCat = await _mediator.Send(new AddCatCommand(newCat));

            //Return OK with createdbird
            return Ok(createdCat);
        }

        // Update a specific cat by id
        [HttpPut]
        [Authorize]
        [Route("updateCat/{updatedCatId}")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            var updateCatCommand = new UpdateCatByIdCommand(updatedCat, updatedCatId);
            var validationResult = await new UpdateCatByIdCommandValidator().ValidateAsync(updateCatCommand);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
            }

            var updatedCatResult = await _mediator.Send(updateCatCommand);

            if (updatedCatResult == null)
            {
                return NotFound($"Cat with ID {updatedCatId} not found.");
            }

            return Ok(updatedCatResult);
        }

        //Delete a specific cat by id
        [HttpDelete]
        [Authorize(Roles = ("Admin"))]
        [Route("deleteCatById/{catId}")]
        public async Task<IActionResult> DeleteCatById(Guid catId)
        {
            var deletedCat = await _mediator.Send(new DeleteCatByIdCommand(catId));

            if (deletedCat == null)
            {
                return NotFound($"Cat with ID {catId} not found.");
            }

            return Ok(deletedCat);
        }

    }
}
