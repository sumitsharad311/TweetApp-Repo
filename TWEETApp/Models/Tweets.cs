using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TWEETApp.Models
{
    public class Tweets
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Tweetid
        {
            get { return Convert.ToString(Id); }           
        }
        public string Tweet { get; set; }
        public int Like { get; set; }
        public string PostedBy { get; set; }
        public List<Comments> tweetcomments { get; set; }
    }
    public class LikeTweet
    {
        public int Like { get; set; }
    }
}
