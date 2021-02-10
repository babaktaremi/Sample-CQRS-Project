using System;
using MediatR;

namespace Sample.Core.MovieApplication.Notifications
{
   public class AddReadModelNotification:INotification
    {
        public string MovieName { get; set; }
        public DateTime PublishYear { get; set; }
        public float ImdbRate { get; set; }
        public float BoxOffice { get; set; }

        public AddReadModelNotification(string movieName, DateTime publishYear, float imdbRate, float boxOffice)
        {
            MovieName = movieName;
            PublishYear = publishYear;
            ImdbRate = imdbRate;
            BoxOffice = boxOffice;
        }

    }
}
