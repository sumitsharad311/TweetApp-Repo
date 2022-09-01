using TWEETApp.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace TWEETApp.Services
{
    public interface ITweetService
    {
        public string PostTweet(Tweets tweet);
        public List<Tweets> ViewMyTweets(string email);
        public List<Tweets> ViewAllTweets();
        public void UpdateTweet(Tweets tweet);
        public void DeleteTweet(ObjectId id, string userName);
        public void LikeTweet(ObjectId id, string userName, LikeTweet like);
        public void ReplyATweet(ObjectId id, string userName, Comments comment);

    }
}
