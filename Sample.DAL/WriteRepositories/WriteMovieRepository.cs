using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.DAL.Model.WriteModels;

namespace Sample.DAL.WriteRepositories
{
    public class WriteMovieRepository
    {
        private readonly ApplicationDbContext _db;

        public WriteMovieRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddMovieAsync(Movie movie, CancellationToken cancellationToken = default)
        {
            await _db.Movies.AddAsync(movie, cancellationToken);
        }

        public Task<Movie> GetMovieByIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return _db.Movies.Include(c => c.Director).FirstOrDefaultAsync(c => c.Id == movieId, cancellationToken);
        }

        public void DeleteMovie(Movie movie)
        {
            _db.Movies.Remove(movie);
        }
    }
}