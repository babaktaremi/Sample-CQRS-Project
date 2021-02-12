using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.BackgroundWorker.Channels
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

        public async Task AddToChannelAsync(Movie urgentError, CancellationToken cancellationToken)
        {
            await _serviceChannel.Writer.WriteAsync(urgentError, cancellationToken);
        }

        public IAsyncEnumerable<Movie> ReturnValue(CancellationToken cancellationToken)
        {
            return _serviceChannel.Reader.ReadAllAsync(cancellationToken);
        }
    }
}
