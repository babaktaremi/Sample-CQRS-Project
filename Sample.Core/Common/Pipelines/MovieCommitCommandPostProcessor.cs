using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Sample.Core.MovieApplication.Commands.AddMovie;
using Sample.Core.MovieApplication.Notifications.AddReadMovieNotification;
using Sample.DAL;

namespace Sample.Core.Common.Pipelines
{
    public class MovieCommitCommandPostProcessor : IRequestPostProcessor<AddMovieCommand, AddMovieCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMediator _mediator;

        public MovieCommitCommandPostProcessor(ApplicationDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        public async Task Process(AddMovieCommand request, AddMovieCommandResult response, CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AddReadModelNotification(response.MovieId), cancellationToken);
        }
    }
}