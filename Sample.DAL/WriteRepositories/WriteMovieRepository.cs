using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
   }
}
