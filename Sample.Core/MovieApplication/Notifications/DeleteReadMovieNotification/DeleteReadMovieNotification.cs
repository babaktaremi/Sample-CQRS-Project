using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Sample.DAL.Model.ReadModels;

namespace Sample.Core.MovieApplication.Notifications.DeleteReadMovieNotification
{
   public class DeleteReadMovieNotification:INotification
    {
        public int MovieId { get; }


        public DeleteReadMovieNotification(int movieId)
        {
            MovieId=movieId;
        }
    }
}
