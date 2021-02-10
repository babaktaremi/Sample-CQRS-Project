using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.Commands;
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

            if (command)
                return Ok();

            return BadRequest();
        }

        [HttpGet("GetMovieByName")]
        public async Task<IActionResult> GetMovieByName(string movieName)
        {
            var query = await _mediator.Send(new GetMovieByNameQuery {MovieName = movieName});

            return Ok(query);
        }
    }
}
