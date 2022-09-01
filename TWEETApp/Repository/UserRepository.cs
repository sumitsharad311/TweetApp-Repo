using TWEETApp.Models;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using TWEETApp.Databasesettings;

namespace TWEETApp.Repository
{
   public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Users> collection;
        public UserRepository(IUsersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            collection = database.GetCollection<Users>(settings.UsersCollectionName);
        }
        public string AddUser(Users user)
        {
            try
            {
                collection.InsertOne(user);
                return $"\n Hi {user.first_name} {user.last_name}, Your Account has been created Successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }
        }
        public bool ChangeStatusToTrue(string email)
        {
            try
            {
                Users user = GetUsers(email);

                collection.FindOneAndUpdate<Users>(Builders<Users>.Filter.Eq("email", email),
                    Builders<Users>.Update.Set("IsActive", true));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return false;
            }
        }
        public bool ChangeStatusToFalse(string email)
        {
            try
            {
                Users user = GetUsers(email);

                collection.FindOneAndUpdate<Users>(Builders<Users>.Filter.Eq("email", email),
                    Builders<Users>.Update.Set("IsActive", false));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return false;
            }
        }
        public List<Users> GetAllUsers()
        {
            try
            {
                var users = collection.Find(_ => true).ToList();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }
        }
        public Users GetUsers(string loginID)
        {
            try
            {
                var user = collection.Find(s => s.loginID == loginID).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }

        }
        public Users GetUsersbyEmail(string email)
        {
            try
            {
                var user = collection.Find(s => s.email == email).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }

        }
        public string UpdateUser(Forgot forgot)
        {
            try
            {
                collection.FindOneAndUpdate<Users>(Builders<Users>.Filter.Eq("email", forgot.email),
                Builders<Users>.Update.Set("password", forgot.password));
                collection.FindOneAndUpdate<Users>(Builders<Users>.Filter.Eq("email", forgot.email),
                Builders<Users>.Update.Set("confirmPassword", forgot.confirmPassword));
                return $" Password Updated";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Exception Occured: {ex.Message}");
                return null;
            }
        }
    }
}
