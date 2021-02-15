using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.BackgroundWorker.Common.Channels
{
   public class DeleteModelChannel
    {
        public int MovieId { get; set; }

    }
}
