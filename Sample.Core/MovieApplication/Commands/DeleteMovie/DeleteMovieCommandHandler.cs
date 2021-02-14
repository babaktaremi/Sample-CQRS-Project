using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.Notifications.DeleteReadMovieNotification;
using Sample.DAL.WriteRepositories;

namespace Sample.Core.MovieApplication.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, bool>
    {
        private readonly WriteMovieRepository _writeMovieRepository;
        private readonly IMediator _mediator;

        public DeleteMovieCommandHandler(WriteMovieRepository writeMovieRepository, IMediator mediator)
        {
            _writeMovieRepository = writeMovieRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _writeMovieRepository.GetMovieById(request.MovieId, cancellationToken);

            if (movie is null)
                return false;

            _writeMovieRepository.DeleteMovie(movie);

            await _mediator.Publish(new DeleteReadMovieNotification(movie.Id), cancellationToken);

            return true;
        }
    }
}
