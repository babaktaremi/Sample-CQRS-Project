using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.Common.BaseChannel;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Events;
using Sample.DAL;
using Sample.DAL.WriteRepositories;

namespace Sample.Core.MovieApplication.Commands.DeleteMovie
{
   public class DeleteMovieCommandHandler:IRequestHandler<DeleteMovieCommand,bool>
   {
       private readonly WriteMovieRepository _writeMovieRepository;
       private readonly ApplicationDbContext _db;
       private readonly ChannelQueue<MovieDeleted> _channelQueue;

       public DeleteMovieCommandHandler(WriteMovieRepository writeMovieRepository, ApplicationDbContext db, ChannelQueue<MovieDeleted> channelQueue)
       {
           _writeMovieRepository = writeMovieRepository;
           _db = db;
           _channelQueue = channelQueue;
       }

    public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _writeMovieRepository.GetMovieByIdAsync(request.MovieId, cancellationToken);

            if (movie is null)
                return false;

            _writeMovieRepository.DeleteMovie(movie);

            await _db.SaveChangesAsync(cancellationToken);

            await _channelQueue.AddToChannelAsync(new MovieDeleted { MovieId = request.MovieId }, cancellationToken);


            return true;
        }
    }
}