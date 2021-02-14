using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.DAL.Model.ReadModels
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("movieId")]
        public int MovieId { get; set; }

        [BsonElement("publishYear")]
        public DateTime PublishYear { get; set; }

        [BsonElement("imdbRate")]
        public decimal ImdbRate { get; set; }

        [BsonElement("boxOffice")]
        public decimal BoxOffice { get; set; }

        [BsonElement("director")]
        public string Director { get; set; }
    }
}
