using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TWEETApp.Models
{
    public class Users
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Userid
        {
            get { return Convert.ToString(Id); }
        }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string loginID { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string contactNumber { get; set; }
        public bool IsActive { get; set; }
    }
    public class Login
    {
        [Required]
        public string loginID { get; set; }
        [Required]
        public string password { get; set; }
    }
    public class Forgot
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string confirmPassword { get; set; }

    }
}
