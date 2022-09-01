using TWEETApp.Models;
using MongoDB.Driver;

namespace TWEETApp.Context
{
    public interface IMongoContext
    {
        public IMongoCollection<Users> Users();
        public IMongoCollection<Tweets> Tweets();
    }
}