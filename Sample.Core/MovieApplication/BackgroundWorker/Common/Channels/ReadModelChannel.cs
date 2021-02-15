using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
    public class ReadModelChannel
    {
        public int MovieId { get; set; }
    }
}
