using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class DeleteModelChannel
    {
        private readonly Channel<int> _serviceChannel;

        public DeleteModelChannel()
        {
            _serviceChannel = Channel.CreateBounded<int>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddAsync(int movieId, CancellationToken cancellationToken = default)
        {
            await _serviceChannel.Writer.WriteAsync(movieId, cancellationToken);
        }

        public IAsyncEnumerable<int> ReadAsync(CancellationToken cancellationToken = default)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}