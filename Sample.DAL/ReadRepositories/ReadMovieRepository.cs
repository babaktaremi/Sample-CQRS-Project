using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories.Common;

namespace Sample.DAL.ReadRepositories
{
    public class ReadMovieRepository : BaseReadRepository<Movie>
    {
        public ReadMovieRepository(string connectionString,string database):base(connectionString, database)
        {
            
        }

        public async Task AddMovie(Movie movie)
        {
            await base.Create(movie);
        }

        public Task<Movie> GetMovieByName(string name,CancellationToken cancellationToken)
        {
            return base.GetSingleWithFilter(movie =>movie.Name==name,cancellationToken );
        }

        public Task DeleteMovieById(int movieId,CancellationToken cancellationToken)
        {
            return base.Delete(m => m.MovieId == movieId,cancellationToken);
        }

        public Task<Movie> GetMovieById(int movieId,CancellationToken cancellationToken)
        {
            return base.GetSingleWithFilter(movie => movie.MovieId == movieId,cancellationToken);
        }
    }
}
