using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.DAL.Model.ReadModels
{
   public class Movie_Read
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("publishYear")]
        public DateTime PublishYear { get; set; }

        [BsonElement("imdbRate")]
        public float ImdbRate { get; set; }

        [BsonElement("boxOffice")]
        public float BoxOffice { get; set; }
    }
}
