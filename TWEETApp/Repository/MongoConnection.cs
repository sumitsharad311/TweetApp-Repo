using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TWEETApp.Repository
{
    class MongoConnection
    {
        private static IConfiguration configuration;
        public MongoConnection(IConfiguration config)
        {
            configuration = config;
        }
        public static IMongoDatabase GetDatabase(string dbName)
        {
            string connectionString = configuration.GetConnectionString(dbName);
            MongoClient client = new MongoClient(connectionString);
            return client.GetDatabase(dbName);
        }
    }
}
