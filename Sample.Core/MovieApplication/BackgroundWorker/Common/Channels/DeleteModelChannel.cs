using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class DeleteModelChannel
    {
        public int MovieId { get; set; }

    }
}