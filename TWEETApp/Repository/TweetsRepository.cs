using TWEETApp.Databasesettings;
using TWEETApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace TWEETApp.Repository
{
    public class TweetsRepository : ITweetsRepository
    {
        private readonly IMongoCollection<Tweets> collection;
        public TweetsRepository(ITweetsDatabasesettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<Tweets>(settings.TweetsCollectionName);

        }
        public string AddTweet(Tweets tweet)
        {
            try
            {
                collection.InsertOne(tweet);
                return "\n Tweet Posted";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }
        }
        public Tweets GetTweetByIdandUsername(ObjectId id, string userName)
        {
            var getTweet = collection.Find(s => s.Id == id && s.PostedBy == userName).FirstOrDefault();
            return getTweet;
        }
        public string UpdateATweet(Tweets tweet)
        {
            var filter = Builders<Tweets>.Filter.Eq(p => p.Id, tweet.Id);
            var update = Builders<Tweets>.Update.Set(p => p.Tweet, tweet.Tweet);
            var options = new UpdateOptions { IsUpsert = true };
            collection.UpdateOne(filter, update, options);

            return "Success";
        }
        public string LikeATweet(Tweets tweet)
        {
            var filter = Builders<Tweets>.Filter.Eq(p => p.Id, tweet.Id);
            var update = Builders<Tweets>.Update.Set(p => p.Like, tweet.Like);
            var options = new UpdateOptions { IsUpsert = true };
            collection.UpdateOne(filter, update, options);

            return "Success";
        }
        public string ReplyATweet(Tweets tweet, Comments comment)
        {
            List<Comments> ls = new List<Comments>();
            var filter = Builders<Tweets>.Filter.Eq(p => p.Id, tweet.Id);
            UpdateDefinition<Tweets> update;

            if (tweet.tweetcomments != null)
            {
                ls = tweet.tweetcomments;
                ls.Add(comment);
                update = Builders<Tweets>.Update.Set(p => p.tweetcomments, ls);
            }
            else
                update = Builders<Tweets>.Update.Set(p => p.tweetcomments, new List<Comments> { comment });


            var options = new UpdateOptions { IsUpsert = true };
            collection.UpdateOne(filter, update, options);

            return "Success";
        }
        public List<Tweets> GetAllTweets()
        {
            try
            {
                var tweets = collection.Find(_ => true).ToList();
                return tweets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }

        }
        public List<Tweets> GetMyTweets(string email)
        {
            try
            {
                var tweet = collection.Find(s => s.PostedBy == email).ToList();
                return tweet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }

        }
        public void DeleteATweet(Tweets tweet)
        {

            collection.DeleteOne(s => s.Id == tweet.Id && s.PostedBy == tweet.PostedBy);
        }
    }
}
