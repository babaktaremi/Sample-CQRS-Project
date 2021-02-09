using System;

namespace Sample.DAL.Model.WriteModels
{
   public class Movie_Write
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishYear { get; set; }
        public float ImdbRate { get; set; }
        public float BoxOffice { get; set; }

    }
}
