using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Sample.Core.Common.BaseChannel;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;
using Sample.Core.MovieApplication.Commands.DeleteMovie;
using Sample.DAL;

namespace Sample.Core.Common.Pipelines
{
   public class MovieDeleteCommandPostProcessor:IRequestPostProcessor<DeleteMovieCommand,bool>
   {
       private readonly ApplicationDbContext _db;
       private readonly ChannelQueue<DeleteModelChannel> _channelQueue;

       public MovieDeleteCommandPostProcessor(ApplicationDbContext db, ChannelQueue<DeleteModelChannel> channelQueue)
       {
           _db = db;
           _channelQueue = channelQueue;
       }

        public async Task Process(DeleteMovieCommand request, bool response, CancellationToken cancellationToken)
        {
            await _db.SaveChangesAsync(cancellationToken);

            await _channelQueue.AddToChannelAsync(new DeleteModelChannel { MovieId = request.MovieId }, cancellationToken);
        }
    }
}
