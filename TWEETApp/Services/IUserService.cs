using TWEETApp.Models;
using System.Collections.Generic;

namespace TWEETApp.Services
{
    public interface IUserServices
    {
        public string Register(Users user);
        public string Login(Login login);
        public string ForgotPassword(Forgot forgot);
        public string ResetPassword(string userName, Forgot forgot);
        public List<Users> ViewAllUsers();
        public Users GetOneUser(string userName);
        public bool Logout(string email);

    }
}
