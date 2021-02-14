using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class DeleteModelChannel
    {
        private Channel<int> _serviceChannel;

        public DeleteModelChannel()
        {
            _serviceChannel = Channel.CreateBounded<int>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddToChannelAsync(int movieId, CancellationToken cancellationToken)
        {
            await _serviceChannel.Writer.WriteAsync(movieId, cancellationToken);
        }

        public IAsyncEnumerable<int> ReturnValue(CancellationToken cancellationToken)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
