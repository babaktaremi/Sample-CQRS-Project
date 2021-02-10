using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories.Common;

namespace Sample.DAL.ReadRepositories
{
    public class ReadMovieRepository : BaseReadRepository<Movie_Read>
    {
        public ReadMovieRepository():base("mongodb://localhost:27017", "moviesdatabase")
        {
            
        }

        public async Task AddMovie(Movie_Read movie)
        {
            await base.Create(movie);
        }

        public Task<Movie_Read> GetMovieByName(string name)
        {
            return base.GetSingleWithFilter(new ExpressionFilterDefinition<Movie_Read>(read => read.Name == name));
        }
    }
}
