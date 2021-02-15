using System;
using System.Collections.Generic;
using System.Text;
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
       private readonly ChannelQueue<DeleteModelChannel> _channelQueue;

       public DeleteMovieCommandHandler(WriteMovieRepository writeMovieRepository, ChannelQueue<DeleteModelChannel> channelQueue)
       {
           _writeMovieRepository = writeMovieRepository;
           _channelQueue = channelQueue;
       }

        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _writeMovieRepository.GetMovieById(request.MovieId,cancellationToken);

            if (movie is null)
                return false;

            _writeMovieRepository.DeleteMovie(movie);

            await _channelQueue.AddToChannelAsync(new DeleteModelChannel{MovieId = movie.Id}, cancellationToken);

            return true;
        }
    }
}
