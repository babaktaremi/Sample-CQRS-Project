using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.Common.BaseChannel
{
    public class ChannelQueue<TQueue> where TQueue : class
    {
        private Channel<TQueue> _serviceChannel;

        public ChannelQueue()
        {
            _serviceChannel = Channel.CreateBounded<TQueue>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddToChannelAsync(TQueue model, CancellationToken cancellationToken)
        {
            await _serviceChannel.Writer.WriteAsync(model, cancellationToken);
        }

        public IAsyncEnumerable<TQueue> ReturnValue(CancellationToken cancellationToken)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
