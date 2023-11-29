using Application.Commands.Birds.AddBird;
using Application.Commands.Birds.DeleteBird;
using Application.Commands.Birds.UpdateBird;
using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
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
        [Authorize]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }


        // Update a specific bird by id
        [HttpPut]
        [Authorize]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));
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
