using System.Threading;
using System.Threading.Tasks;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories.Common;

namespace Sample.DAL.ReadRepositories
{
    public class ReadMovieRepository : BaseReadRepository<Movie>
    {
        public ReadMovieRepository(string connectionString, string database)
            : base(connectionString, database)
        {
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await base.CreateAsync(movie);
        }

        public Task<Movie> GetMovieByNameAsync(string name, CancellationToken cancellationToken)
        {
            return base.GetSingleWithFilterAsync(movie => movie.Name == name, cancellationToken);
        }

        public Task DeleteMovieByIdAsync(int movieId, CancellationToken cancellationToken)
        {
            return base.DeleteAsync(m => m.MovieId == movieId, cancellationToken);
        }

        public Task<Movie> GetMovieByIdAsync(int movieId, CancellationToken cancellationToken)
        {
            return base.GetSingleWithFilterAsync(movie => movie.MovieId == movieId, cancellationToken);
        }
    }
}
