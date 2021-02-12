﻿using System;
using MediatR;

namespace Sample.Core.MovieApplication.Notifications
{
   public class AddReadModelNotification:INotification
    {
        public string MovieName { get; }
        public DateTime PublishYear { get; }
        public decimal ImdbRate { get;  }
        public decimal BoxOffice { get;  }
        public string Director { get;  }


        public AddReadModelNotification(string movieName, DateTime publishYear, decimal imdbRate, decimal boxOffice,string director)
        {
            MovieName = movieName;
            PublishYear = publishYear;
            ImdbRate = imdbRate;
            BoxOffice = boxOffice;
            Director = director;
        }

    }
}
