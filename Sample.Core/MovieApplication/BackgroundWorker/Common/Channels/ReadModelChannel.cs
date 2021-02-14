using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class ReadModelChannel
    {
        private Channel<Movie> _serviceChannel;

        public ReadModelChannel()
        {
            _serviceChannel = Channel.CreateBounded<Movie>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddToChannelAsync(Movie movie, CancellationToken cancellationToken)
        {
            await _serviceChannel.Writer.WriteAsync(movie, cancellationToken);
        }

        public IAsyncEnumerable<Movie> ReturnValue(CancellationToken cancellationToken)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
