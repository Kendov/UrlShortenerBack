using MongoDB.Bson.Serialization.Attributes;

namespace urlShortener.Models
{
    public class UrlData
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Id { get; set; }
        
        [BsonRequired]
        public string Url { get; set; }
    }
}