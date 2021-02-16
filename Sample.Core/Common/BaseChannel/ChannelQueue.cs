using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.Common.BaseChannel
{
    public class ChannelQueue<TMessage> where TMessage : class
    {
        private Channel<TMessage> _serviceChannel;

        public ChannelQueue()
        {
            _serviceChannel = Channel.CreateBounded<TMessage>(new BoundedChannelOptions(4000)
            {
                SingleReader = false,
                SingleWriter = false
            });
        }

        public async Task AddToChannelAsync(TMessage model, CancellationToken cancellationToken)
        {
            await _serviceChannel.Writer.WriteAsync(model, cancellationToken);
        }

        public IAsyncEnumerable<TMessage> ReturnValue(CancellationToken cancellationToken)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
