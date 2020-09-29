using MongoDB.Bson.Serialization.Attributes;

namespace urlShortener.Models
{
    public class UrlDataEntryModel
    {
        public string CustomUrl { get; set; }
        
        [BsonRequired]
        public string Url { get; set; }

        [BsonRequired]
        public int ExpirationTime { get; set; }

    }
}