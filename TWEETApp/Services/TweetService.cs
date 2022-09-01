using TWEETApp.Repository;
using TWEETApp.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace TWEETApp.Services
{
    public class TweetService : ITweetService
    {
        private ITweetsRepository tweetsRepository;
        public TweetService(ITweetsRepository tweetsRepository)
        {
            this.tweetsRepository = tweetsRepository;
        }
        public string PostTweet(Tweets tweet)
        {
            try
            {
                var response = tweetsRepository.AddTweet(tweet);
                if (!string.IsNullOrEmpty(response))
                    return response;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public void UpdateTweet(Tweets tweet)
        {
            var getTweet = tweetsRepository.GetTweetByIdandUsername(tweet.Id, tweet.PostedBy);
            if (getTweet != null)
                tweetsRepository.UpdateATweet(tweet);
        }

        public List<Tweets> ViewAllTweets()
        {
            try
            {
                var tweets = tweetsRepository.GetAllTweets();
                if (tweets.Count != 0)
                    return tweets;
                else
                    Console.WriteLine("\n There are no tweets yet.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public List<Tweets> ViewMyTweets(string email)
        {
            try
            {
                var tweets = tweetsRepository.GetMyTweets(email);
                if (tweets.Count != 0)
                    return tweets;
                else
                    Console.WriteLine("\n You have not posted any tweet yet.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public void DeleteTweet(ObjectId id, string userName)
        {
            Tweets tweet = new Tweets { Id = id, PostedBy = userName };
            tweetsRepository.DeleteATweet(tweet);

        }
        public void LikeTweet(ObjectId id, string userName, LikeTweet like)
        {
            var getTweet = tweetsRepository.GetTweetByIdandUsername(id, userName);

            if (getTweet != null)
            {
                getTweet.Like++;
                tweetsRepository.LikeATweet(getTweet);
            }

        }
        public void ReplyATweet(ObjectId id, string userName, Comments comment)
        {
            var getTweet = tweetsRepository.GetTweetByIdandUsername(id, userName);

            if (getTweet != null)
            {
                tweetsRepository.ReplyATweet(getTweet, comment);
            }
        }
    }
}
