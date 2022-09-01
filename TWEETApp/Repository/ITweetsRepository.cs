using TWEETApp.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace TWEETApp.Repository
{
    public interface ITweetsRepository
    {
        public string AddTweet(Tweets tweet);
        public List<Tweets> GetMyTweets(string email);
        public List<Tweets> GetAllTweets();
        public Tweets GetTweetByIdandUsername(ObjectId id, string userName);
        public string UpdateATweet(Tweets tweet);
        public void DeleteATweet(Tweets tweet);
        public string LikeATweet(Tweets tweet);
        public string ReplyATweet(Tweets tweet, Comments comment);
    }
}
