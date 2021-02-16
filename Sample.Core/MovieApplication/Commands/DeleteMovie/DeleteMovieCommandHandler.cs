using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.Common.BaseChannel;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;
using Sample.DAL.WriteRepositories;

namespace Sample.Core.MovieApplication.Commands.DeleteMovie
{
   public class DeleteMovieCommandHandler:IRequestHandler<DeleteMovieCommand,bool>
   {
       private readonly WriteMovieRepository _writeMovieRepository;

       public DeleteMovieCommandHandler(WriteMovieRepository writeMovieRepository)
       {
           _writeMovieRepository = writeMovieRepository;
       }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _writeMovieRepository.GetMovieByIdAsync(request.MovieId, cancellationToken);

            if (movie is null)
                return false;

            _writeMovieRepository.DeleteMovie(movie);

            return true;
        }
    }
}