using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories;
using Sample.DAL.WriteRepositories;

namespace Sample.Core.MovieApplication.BackgroundWorker.AddReadMovie
{
    public class AddReadModelWorker : BackgroundService
    {
        private readonly ReadMovieRepository _readMovieRepository;
        private readonly ReadModelChannel _readModelChannel;
        private readonly ILogger<AddReadModelWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AddReadModelWorker(ReadMovieRepository readMovieRepository, ReadModelChannel readModelChannel, ILogger<AddReadModelWorker> logger, IServiceProvider serviceProvider)
        {
            _readMovieRepository = readMovieRepository;
            _readModelChannel = readModelChannel;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var writeRepository = scope.ServiceProvider.GetRequiredService<WriteMovieRepository>();

                try
                {
                    await foreach (var item in _readModelChannel.ReadAsync(stoppingToken))
                    {
                        var movie = await writeRepository.GetMovieByIdAsync(item, stoppingToken);

                        if (movie != null)
                        {
                            await _readMovieRepository.AddAsync(new Movie
                            {
                                MovieId = movie.Id,
                                Director = movie.Director.FullName,
                                Name = movie.Name,
                                PublishYear = movie.PublishYear,
                                BoxOffice = movie.BoxOffice,
                                ImdbRate = movie.ImdbRate
                            }, stoppingToken);
                        }
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