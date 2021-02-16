using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories.Common;

namespace Sample.DAL.ReadRepositories
{
    public class ReadMovieRepository : BaseReadRepository<Movie>
    {
        public ReadMovieRepository(IMongoDatabase db) : base(db)
        {
        }

        public Task<Movie> GetByMovieIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(movie => movie.MovieId == movieId, cancellationToken);
        }

        public Task<Movie> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(movie => movie.Name == name, cancellationToken);
        }

        public Task DeleteByMovieIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(m => m.MovieId == movieId, cancellationToken);
        }
    }
}