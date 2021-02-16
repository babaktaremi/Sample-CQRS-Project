using MediatR;

namespace Sample.Core.MovieApplication.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest<bool>
    {
        public int MovieId { get; set; }
    }
}