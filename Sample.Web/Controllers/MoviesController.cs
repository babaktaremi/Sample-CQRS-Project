using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.Commands;
using Sample.Core.MovieApplication.Commands.AddMovie;
using Sample.Core.MovieApplication.Commands.DeleteMovie;
using Sample.Core.MovieApplication.Queries.GetMovieByName;

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
        public async Task<IActionResult> AddMovie(AddMovieCommand model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = await _mediator.Send(model);


            return Ok(command.MovieId);
        }

        [HttpGet("GetMovieByName")]
        public async Task<IActionResult> GetMovieByName([FromQuery] GetMovieByNameQuery model)
        {
            var query = await _mediator.Send(model);

            return Ok(query);
        }

        [HttpPost("DeleteMovie")]
        public async Task<IActionResult> DeleteMovie(DeleteMovieCommand model)
        {
            var result = await _mediator.Send(model);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
