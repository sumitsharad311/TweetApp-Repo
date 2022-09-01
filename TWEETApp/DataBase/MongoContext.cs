using TWEETApp.Models;
using MongoDB.Driver;

namespace TWEETApp.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase _database;
        public MongoContext(IMongoDatabase database)
        {
            _database = database;
        }
        public IMongoCollection<Users> Users()
        {
            try
            {
                return _database.GetCollection<Users>("Users");
            }
            catch
            {
                return null;
            }
        }
        public IMongoCollection<Tweets> Tweets()
        {
            try
            {
                return _database.GetCollection<Tweets>("Tweets");
            }
            catch
            {
                return null;
            }
        }
    }
}
