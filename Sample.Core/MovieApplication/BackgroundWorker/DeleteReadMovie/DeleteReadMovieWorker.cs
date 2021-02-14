using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Core.MovieApplication.BackgroundWorker.AddReadMovie;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;
using Sample.DAL.ReadRepositories;

namespace Sample.Core.MovieApplication.BackgroundWorker.DeleteReadMovie
{
    public class DeleteReadMovieWorker : BackgroundService
    {
        private readonly ReadMovieRepository _readMovieRepository;
        private readonly DeleteModelChannel _deleteModelChannel;
        private readonly ILogger<AddReadModelWorker> _logger;

        public DeleteReadMovieWorker(ReadMovieRepository readMovieRepository, DeleteModelChannel deleteModelChannel, ILogger<AddReadModelWorker> logger)
        {
            _readMovieRepository = readMovieRepository;
            _deleteModelChannel = deleteModelChannel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await foreach (var item in _deleteModelChannel.ReturnValue(stoppingToken))
                    {
                        await _readMovieRepository.DeleteMovieById(item, stoppingToken);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}
