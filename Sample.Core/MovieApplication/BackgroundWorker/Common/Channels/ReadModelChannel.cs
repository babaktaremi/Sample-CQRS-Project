using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class ReadModelChannel
    {
        private Channel<int> _serviceChannel;

        public ReadModelChannel()
        {
            _serviceChannel = Channel.CreateBounded<int>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddToChannelAsync(int movie, CancellationToken cancellationToken = default)
        {
            await _serviceChannel.Writer.WriteAsync(movie, cancellationToken);
        }

        public IAsyncEnumerable<int> ReturnValueAsync(CancellationToken cancellationToken = default)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}