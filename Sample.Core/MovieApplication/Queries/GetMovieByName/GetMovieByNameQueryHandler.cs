using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories;

namespace Sample.Core.MovieApplication.Queries.GetMovieByName
{
    public class GetMovieByNameQueryHandler : IRequestHandler<GetMovieByNameQuery, Movie>
    {
        private readonly ReadMovieRepository _readMovieRepository;

        public GetMovieByNameQueryHandler(ReadMovieRepository readMovieRepository)
        {
            _readMovieRepository = readMovieRepository;
        }

        public Task<Movie> Handle(GetMovieByNameQuery request, CancellationToken cancellationToken)
        {
            return _readMovieRepository.GetMovieByNameAsync(request.MovieName, cancellationToken);
        }
    }
}