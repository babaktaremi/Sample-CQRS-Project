using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Sample.Core.Common.BaseChannel;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Events;
using Sample.Core.MovieApplication.Commands.AddMovie;
using Sample.DAL;

namespace Sample.Core.Common.Pipelines
{
    public class MovieCommitCommandPostProcessor : IRequestPostProcessor<AddMovieCommand, AddMovieCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly ChannelQueue<MovieAdded> _channel;

        public MovieCommitCommandPostProcessor(ApplicationDbContext db, ChannelQueue<MovieAdded> channel)
        {
            _db = db;
            _channel = channel;
        }

        public async Task Process(AddMovieCommand request, AddMovieCommandResult response, CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken);

            await _channel.AddToChannelAsync(new MovieAdded{MovieId = response.MovieId}, cancellationToken);
        }
    }
}