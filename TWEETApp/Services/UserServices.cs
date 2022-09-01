using TWEETApp.Repository;
using TWEETApp.Models;
using System;
using System.Collections.Generic;
using TWEETApp.Exceptions;

namespace TWEETApp.Services
{
    public class UserServices : IUserServices
    {
        private IUserRepository userRepository;
        private DataValidationServices dataValidationServices = new DataValidationServices();
        public UserServices(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public string Login(Login login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.loginID) || string.IsNullOrEmpty(login.password))
                {
                    //Console.WriteLine($"\n Email and Password are required fields.");
                    //return null;
                    return $"\n LoginID and Password are required fields.";
                }

                var getUser = userRepository.GetUsers(login.loginID);

                if (getUser == null)
                    //throw new CustomException($" No account has been found with mail id {user.email}");
                    return null;
                    //return $" No account has been found with mail id {login.loginID}";

                else if (getUser.password == login.password)
                {
                    var isTrue = userRepository.ChangeStatusToTrue(login.loginID);
                    if (isTrue)
                        return $" Hi {getUser.first_name} {getUser.last_name}, You are Logged in now.";
                    return getUser.email;
                }
                else
                    Console.WriteLine("\n Wrong Password.");

                return null;
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"\n {ex.Message}");
                return null;
            }
        }
        public string Register(Users user)
        {
            try
            {
                var userbyloginid = userRepository.GetUsers(user.loginID);
                if (userbyloginid != null)
                {
                    return "User Already exist";
                }

                var userbyemailid = userRepository.GetUsersbyEmail(user.email);
                if (userbyemailid != null)
                {
                    return "User Already exist";
                }

                if (user.contactNumber.Length != 10)
                {
                    return "Mobile number should be 10 digit";
                }

                if (string.IsNullOrEmpty(user.first_name) || string.IsNullOrEmpty(user.contactNumber) || string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.loginID) || string.IsNullOrEmpty(user.password) || string.IsNullOrEmpty(user.confirmPassword))
                {
                    throw new CustomException(" First Name, Email, loginID, Mobile Number, Password are required fileds");
                }
                var message = userRepository.AddUser(user);

                if (!string.IsNullOrEmpty(message) && user.password == user.confirmPassword)
                    return message;
                else
                    return "Password not matched";
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"\n {ex.Message}");
                return null;
            }

        }
        public string ForgotPassword(Forgot forgot)
        {
            try
            {
                if (string.IsNullOrEmpty(forgot.email))
                {
                    Console.WriteLine($"\n Email is a required field.");
                    return null;
                }

                var getUser = userRepository.GetUsers(forgot.email);

                if (getUser == null)
                {
                    Console.WriteLine($"\n No account has been found with mail id {forgot.email}");
                    return null;
                }
                return "Success";

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n " + ex.Message);
                return null;
            }

        }
        public string ResetPassword(string userName, Forgot forgot)
        {
            try
            {
                var getUser = userRepository.GetUsersbyEmail(userName);


                if (getUser != null && userName == getUser.email && userName == forgot.email)
                {
                    var isUpdated = userRepository.UpdateUser(forgot);
                    if (!string.IsNullOrEmpty(isUpdated))
                        return isUpdated;
                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n " + ex.Message);
                return null;
            }
        }
        public List<Users> ViewAllUsers()
        {
            try
            {
                var users = userRepository.GetAllUsers();
                if (users.Count != 0)
                    return users;
                else
                    Console.WriteLine("\n No users yet.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n " + ex.Message);
                return null;
            }

        }
        public Users GetOneUser(string UserName)
        {
            try
            {
                var users = userRepository.GetUsers(UserName);
                if (users != null)
                    return users;
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n " + ex.Message);
                return null;
            }

        }
        public bool Logout(string email)
        {
            try
            {
                var response = userRepository.ChangeStatusToFalse(email);
                if (response.Equals(true))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n " + ex.Message);
                return false;
            }
        }
    }
}
