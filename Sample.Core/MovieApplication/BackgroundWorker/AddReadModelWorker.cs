using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sample.Core.MovieApplication.BackgroundWorker.Channels;
using Sample.DAL.ReadRepositories;

namespace Sample.Core.MovieApplication.BackgroundWorker
{
  public class AddReadModelWorker:BackgroundService
  {
      private readonly ReadMovieRepository _readMovieRepository;
      private readonly ReadModelChannel _readModelChannel;
      private readonly ILogger<AddReadModelWorker> _logger;

      public AddReadModelWorker(ReadMovieRepository readMovieRepository, ReadModelChannel readModelChannel, ILogger<AddReadModelWorker> logger)
      {
          _readMovieRepository = readMovieRepository;
          _readModelChannel = readModelChannel;
          _logger = logger;
      }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await foreach (var item in _readModelChannel.ReturnValue(stoppingToken))
                    {
                        await _readMovieRepository.AddMovie(item);
                    }
                }
                catch (Exception e)
                {
                   _logger.LogError(e,e.Message);
                }
            }
        }
    }
}
