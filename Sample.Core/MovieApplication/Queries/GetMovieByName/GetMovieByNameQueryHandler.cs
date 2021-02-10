using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.DAL.Model.ReadModels;
using Sample.DAL.ReadRepositories;

namespace Sample.Core.MovieApplication.Queries.GetMovieByName
{
   public class GetMovieByNameQueryHandler:IRequestHandler<GetMovieByNameQuery,Movie_Read>
   {
       private readonly ReadMovieRepository _readMovieRepository;

       public GetMovieByNameQueryHandler(ReadMovieRepository readMovieRepository)
       {
           _readMovieRepository = readMovieRepository;
       }

        public Task<Movie_Read> Handle(GetMovieByNameQuery request, CancellationToken cancellationToken)
        {
            return _readMovieRepository.GetMovieByName(request.MovieName);
        }
    }
}
