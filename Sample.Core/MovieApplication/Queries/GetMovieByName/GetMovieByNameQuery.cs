using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.Queries.GetMovieByName
{
   public class GetMovieByNameQuery:IRequest<Movie>
    {
        public string MovieName { get; set; }
    }
}
