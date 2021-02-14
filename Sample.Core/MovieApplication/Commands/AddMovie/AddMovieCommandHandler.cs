using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.Notifications;
using Sample.Core.MovieApplication.Notifications.AddReadMovieNotification;
using Sample.DAL.Model.WriteModels;
using Sample.DAL.WriteRepositories;

namespace Sample.Core.MovieApplication.Commands.AddMovie
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly WriteMovieRepository _movieRepository;
        private readonly DirectorRepository _directorRepository;


        public AddMovieCommandHandler(IMediator mediator, WriteMovieRepository movieRepository, DirectorRepository directorRepository)
        {
            _mediator = mediator;
            _movieRepository = movieRepository;
            _directorRepository = directorRepository;
        }

        public async Task<bool> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var director = await _directorRepository.GetDirector(request.Director,cancellationToken);

            if (director is null)
            {
                director = new Director { FullName = request.Director };
                _directorRepository.AddDirector(director);
            }

            var movie = new Movie
            {
                PublishYear = request.PublishYear,
                BoxOffice = request.BoxOffice,
                ImdbRate = request.ImdbRate,
                Name = request.Name,
                Director = director
            };

            _movieRepository.AddMovie(movie);

            var notification = new AddReadModelNotification(request.Name, request.PublishYear, request.ImdbRate,
                request.BoxOffice,request.Director,movie.Id);

            await _mediator.Publish(notification, cancellationToken);

            return true;
        }
    }
}
