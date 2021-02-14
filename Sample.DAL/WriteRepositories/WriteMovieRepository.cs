using System;
using System.Collections.Generic;
using System.Text;
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

       public void AddMovie(Movie movie)
       {
           _db.Movies.Add(movie);
       }

       public Task<Movie> GetMovieById(int movieId,CancellationToken cancellationToken)
       {
           return _db.Movies.Include(c=>c.Director).FirstOrDefaultAsync(c => c.Id == movieId, cancellationToken: cancellationToken);
       }

       public void DeleteMovie(Movie movie)
       {
           _db.Movies.Remove(movie);
       }
   }
}
