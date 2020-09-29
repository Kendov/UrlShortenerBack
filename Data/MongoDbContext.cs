using System;
using MongoDB.Driver;
using urlShortener.Models;

namespace urlShortener.Data
{
    public class MongoDbContext
    {
        public readonly IDbConfiguration DbConfiguration;

        private IMongoDatabase _database { get; }
        
        public MongoDbContext(IDbConfiguration configuration)
        {
            DbConfiguration = configuration;
            var client = new MongoClient(DbConfiguration.ConnectionString);
            _database = client.GetDatabase(DbConfiguration.DatabaseName);

            // uses TTL mongoDB for expiration time
            // define expiration time based on expiration field on urlData
            // https://docs.mongodb.com/manual/tutorial/expire-data/            
            var indexKeysDefinition = Builders<UrlData>.IndexKeys.Ascending(x => x.CreatedAt);
            var indexOptions = new CreateIndexOptions(){ExpireAfter = TimeSpan.Zero};
            var indexModel = new CreateIndexModel<UrlData>(indexKeysDefinition, indexOptions);
            UrlData.Indexes.CreateOne(indexModel);

        }

        public IMongoCollection<UrlData> UrlData { 
            get
            {
                return _database.GetCollection<UrlData>(DbConfiguration.CollectionName);
            } 
        }
        
    }
}