using MongoDB.Driver;
using urlShortener.Models;

namespace urlShortener.Data
{
    public class MongoDbContext
    {
        public static string ConnectionString;
        public static string DatabaseName;
        public static string IsSSL;

        private IMongoDatabase _database { get; }

        public MongoDbContext(IDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            _database = client.GetDatabase(configuration.DatabaseName);

        }

        public IMongoCollection<UrlData> UrlData { 
            get
            {
                return _database.GetCollection<UrlData>("UrlData");
            } 
        }
        
    }
}