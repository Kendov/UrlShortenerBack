using System;
using MongoDB.Bson.Serialization.Attributes;

namespace urlShortener.Models
{
    public class UrlData
    {
        public UrlData()
        {
            CreatedAt = DateTime.Now;
            ExpireAt = CreatedAt.AddMinutes(5);
        }
        public UrlData(int expirationInSec)
        {
            CreatedAt = DateTime.Now;
            ExpireAt = CreatedAt.AddSeconds(expirationInSec);
        }

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; }
        
        [BsonRequired]
        public string Url { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime ExpireAt { get; private set; }
    }
}