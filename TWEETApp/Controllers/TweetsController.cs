using System;
using TWEETApp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TWEETApp.Services;
using Microsoft.AspNetCore.Cors;

namespace TWEETApp.Controllers
{
    [EnableCors()]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        public ITweetService tweetService;

        public TweetsController(ITweetService tweetService)
        {
            this.tweetService = tweetService;
        }
        /// <summary>
        /// This api will fetch all tweets available in database
        /// </summary>
        /// <returns>it wil return all tweets </returns>
        // GET: api/Tweets
        [HttpGet("all")]
        public ObjectResult all()
        {
            try
            {
                return Ok(tweetService.ViewAllTweets());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }
        }
        /// <summary>
        /// It will fetch all the tweets for selected particular user by passing userid as a argument.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>it will return all tweets of selected particular user </returns>
        [HttpGet("username")]
        public ObjectResult GetAllTweetsOfOneUser(string userName)
        {
            try
            {
                return Ok(tweetService.ViewMyTweets(userName));
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { msg = "User Not Found", err = ex });
            }

        }
        /// <summary>
        /// This api will add the tweets for a given username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="tweet"></param>
        /// <returns>It return status of tweet</returns>
        // POST: api/Tweets
        [HttpPost("{username}/add")]
        public ObjectResult PostNewTweet(string username, [FromBody] Tweets tweet)
        {
            try
            {
                if (tweet.Tweet.Length <= 144)
                {
                    tweet.PostedBy = username;
                    tweetService.PostTweet(tweet);
                    return Ok(new { msg = "Successfully added." });
                }
                else
                {
                    return Ok(new { msg = "Cant Add: Tweet word limit is 144 only" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server error.", err = ex });
            }

        }
        /// <summary>
        /// This api will update the tweets for a given username and id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <param name="tweet"></param>
        /// <returns>it will return status of tweet updation</returns>
        // PUT: api/Tweets/5
        [HttpPut("{username}/update/{id}")]
        public ObjectResult UpdateAtweet(string username, string id, [FromBody] Tweets tweet)
        {
            try
            {
                if (tweet.Tweet.Length <= 144)
                {
                    tweet.Id = ObjectId.Parse(id);
                    tweet.PostedBy = username;
                    tweetService.UpdateTweet(tweet);
                    return Ok(new { msg = "Successfully Updated" });
                }
                else
                {
                    return Ok(new { msg = "Cant Reply: Word limit is 144 only" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server error.", err = ex });
            }


        }
        /// <summary>
        /// This api will delete the tweets for a given username and id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <returns>it will return status of tweet updation</returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{username}/delete/{id}")]
        public ObjectResult Delete(string username, string id)
        {
            try
            {
                tweetService.DeleteTweet(ObjectId.Parse(id), username);
                return Ok(new { msg = "Successfully Deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server error.", err = ex });
            }

        }
        /// <summary>
        /// This api will update the like of tweets for a given  id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <param name="like"></param>
        /// <returns>it will return status of tweet updation after doing liked by some other user</returns>
        [HttpPut("{username}/like/{id}")]
        public ObjectResult Like(string username, string id, [FromBody] LikeTweet like)
        {
            try
            {
                tweetService.LikeTweet(ObjectId.Parse(id), username, like);
                return Ok(new { msg = "Successfully Liked." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server error.", err = ex });
            }

        }
        /// <summary>
        /// This api will add the reply of tweets for a given username and id
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <returns>it will return status of tweet updation after doing comment on tweet by some other user</returns>
        [HttpPost("{username}/reply/{id}")]
        public ObjectResult Reply(string username, string id, [FromBody] Comments comment)
        {
            try
            {
                if (comment.Comment.Length <= 144)
                {
                    tweetService.ReplyATweet(ObjectId.Parse(id), username, comment);
                    return Ok(new { msg = "Replied added to the tweet." });
                }
                else
                {
                    return Ok(new { msg = "Replied cant added: Word limit is 144" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server error.", err = ex });
            }

        }
    }
}
