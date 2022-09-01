using TWEETApp.Models;
using System.Collections.Generic;

namespace TWEETApp.Repository
{
    public interface IUserRepository
    {
        public string AddUser(Users user);
        public Users GetUsers(string loginID);
        public Users GetUsersbyEmail(string email);
        public string UpdateUser(Forgot forgot);
        public List<Users> GetAllUsers();
        public bool ChangeStatusToTrue(string email);
        public bool ChangeStatusToFalse(string email);
    }
}
