using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.Commands.AddMovie;
using Sample.Core.MovieApplication.Commands.DeleteMovie;
using Sample.Core.MovieApplication.Queries.GetMovieByName;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace Sample.Web.Controllers
{
    [ApiController]
    [Route("Movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MoviesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddMovie")]
        public async Task<IActionResult> AddMovie(AddMovieCommand model, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(model, cancellationToken);

            return Ok(command.MovieId);
        }

        [HttpGet("GetMovieByName")]
        public async Task<IActionResult> GetMovieByName([FromQuery] GetMovieByNameQuery model, CancellationToken cancellationToken)
        {
            var query = await _mediator.Send(model, cancellationToken);

            return Ok(query);
        }

        [HttpPost("DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(DeleteMovieCommand model, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(model, cancellationToken);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}