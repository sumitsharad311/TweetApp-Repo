using System;
using TWEETApp.Models;
using TWEETApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TWEETApp.Controllers
{
    [EnableCors()]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        /// <summary>
        /// This one api used for getting all users
        /// </summary>
        /// <returns>it will return all users</returns>
        // GET: api/Users
        [HttpGet("all")]
        public ObjectResult getAll()
        {
            try
            {
                return Ok(userServices.ViewAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }

        }
        /// <summary>
        /// This one api used for getting all information related user when their username(userid) is passed as argument
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>it will return particular user information</returns>

        [HttpGet("search/username")]
        public ObjectResult getOne(string userName)
        {

            try
            {
                return Ok(userServices.GetOneUser(userName));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }

        }
        /// <summary>
        /// This one api is used for login purpose of existing users
        /// </summary>
        /// <param name="login"></param>
        /// <returns>it will return message</returns>
        [HttpPost("login")]
        public ObjectResult Login([FromBody] Login login)
        {
            try
            {
                var res = userServices.Login(login);
                if (!string.IsNullOrEmpty(res))
                    return Ok(res);
                else
                    return StatusCode(404, new { msg = "Username/password is incorrect" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }

        }
        /// <summary>
        /// This one api is used for updating new password after forgetting previous one
        /// </summary>
        /// <param name="email"></param>
        /// <param name="forgot"></param>
        /// <returns>It will return object result after updating forgetted password</returns>
        [HttpPut("{email}/forgot")]
        public ObjectResult Forgot(string email, [FromBody] Forgot forgot)
        {
            try
            {
                var res = userServices.ResetPassword(email, forgot);
                if (res == null)
                {
                    return Ok(new { response = "User Not Found" });
                }
                if (forgot.password == forgot.confirmPassword)
                {
                    return Ok(res);
                }
                else
                {
                    return Ok(new { response = "Password not matched" });
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }

        }
        /// <summary>
        /// This one api is used for creating new user if they will not created initially their account
        /// </summary>
        /// <param name="user"></param>
        /// <returns>it will return object result notification after creation of new user </returns>
        // POST: api/Users
        [HttpPost("register")]
        public ObjectResult Register([FromBody] Users user)
        {
            try
            {
                var res = userServices.Register(user);
                if (!string.IsNullOrEmpty(res))
                {
                    return Ok(res);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Internal server Error", err = ex });
            }

        }
    }
}
