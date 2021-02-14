using System;

namespace Sample.DAL.Model.WriteModels
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishYear { get; set; }
        public decimal ImdbRate { get; set; }
        public decimal BoxOffice { get; set; }
        public int DirectorId { get; set; }

        public Director Director { get; set; }
    }
}