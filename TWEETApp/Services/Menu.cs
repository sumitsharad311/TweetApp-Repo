using TWEETApp.Models;
using System;

namespace TWEETApp.Services
{
    public class Menu
    {
        IUserServices userServices;
        ITweetService tweetService;
        public Menu(IUserServices userServices, ITweetService tweetService)
        {
            this.userServices = userServices;
            this.tweetService = tweetService;
        }
        public void Items()
        {
            int selectedItem = int.MinValue;
            DataValidationServices dataValidation = new DataValidationServices();
            do
            {
                try
                {
                    Console.WriteLine("======== Menu ========");
                    Console.WriteLine(" 1. Register");
                    Console.WriteLine(" 2. Login");
                    Console.WriteLine(" 3. Forget Password");
                    Console.WriteLine(" 0. Exit");

                    Console.WriteLine("======================");
                    Console.Write("Input: ");

                    selectedItem = int.Parse(Console.ReadLine());

                    Users user = new Users();
                    Tweets tweet = new Tweets();
                    Login login = new Login();
                    Forgot forgot = new Forgot();

                    switch (selectedItem)
                    {
                        case 1:
                            Console.Write("\n Enter First Name: ");
                            user.first_name = Console.ReadLine();

                            Console.Write(" Enter Last Name: ");
                            user.last_name = Console.ReadLine();

                            Console.Write(" Enter LoginID: ");
                            user.loginID = Console.ReadLine();

                            Console.Write(" Enter Contact Number: ");
                            user.contactNumber = (Console.ReadLine());
                            var response = dataValidation.DateValidation(user.contactNumber);
                            if (!response)
                                throw new Exception("Invalid format");

                            Console.Write(" Enter Email Id: ");
                            user.email = Console.ReadLine();
                            response = dataValidation.EmailValidation(user.email);
                            if (!response)
                                throw new Exception($"{user.email} is not Valid Email Address");

                            Console.Write(" Enter Password: ");
                            user.password = Console.ReadLine();
                            response = dataValidation.PasswordValidation(user.password);
                            if (!response)
                                throw new Exception("");

                            var message = userServices.Register(user);

                            if (!string.IsNullOrEmpty(message))
                                Console.WriteLine(message);
                            break;
                        case 2:
                            Console.Write("\n Enter User Name: ");
                            user.email = Console.ReadLine();

                            Console.Write(" Enter Password: ");
                            user.password = Console.ReadLine();

                            var result = userServices.Login(login);

                            if (!string.IsNullOrEmpty(result))
                            {
                                Console.WriteLine("\n" + result);
                                do
                                {
                                    try
                                    {
                                        Console.WriteLine("======================");
                                        Console.WriteLine(" 1. Post a tweet");
                                        Console.WriteLine(" 2. View my tweets");
                                        Console.WriteLine(" 3. View all tweets");
                                        Console.WriteLine(" 4. View all Users");
                                        Console.WriteLine(" 5. Reset Password");
                                        Console.WriteLine(" 6. Logout");

                                        Console.WriteLine("======================");
                                        Console.Write("Input: ");

                                        selectedItem = int.Parse(Console.ReadLine());

                                        switch (selectedItem)
                                        {
                                            case 1:
                                                Console.Write("\n Write your Tweet: ");
                                                tweet.Tweet = Console.ReadLine();

                                                tweet.PostedBy = user.email;

                                                message = tweetService.PostTweet(tweet);

                                                if (!string.IsNullOrEmpty(message))
                                                    Console.WriteLine(message);
                                                break;


                                            case 2:
                                                var tweets = tweetService.ViewMyTweets(user.email);
                                                if (tweets != null)
                                                {
                                                    foreach (var item in tweets)
                                                    {
                                                        Console.WriteLine($"\n {item.Tweet}");
                                                    }
                                                }
                                                break;

                                            case 3:
                                                tweets = tweetService.ViewAllTweets();
                                                if (tweet != null)
                                                {
                                                    foreach (var item in tweets)
                                                    {
                                                        Console.WriteLine($"\n {item.Tweet} - [posted by {item.PostedBy}]");
                                                    }
                                                }
                                                break;
                                            case 4:
                                                var users = userServices.ViewAllUsers();
                                                int index = 1;
                                                if (user != null)
                                                {
                                                    foreach (var item in users)
                                                    {
                                                        Console.WriteLine($"\n {index++}. User: {item.email}");
                                                    }
                                                }
                                                break;

                                            case 5:
                                                Console.Write("\n Enter Old Pasword: ");
                                                user.password = Console.ReadLine();
                                                message = userServices.ResetPassword("", forgot);

                                                if (!String.IsNullOrEmpty(message))
                                                    Console.WriteLine(message);
                                                break;

                                            case 6:
                                                response = userServices.Logout(user.email);
                                                if (response.Equals(true))
                                                    Console.WriteLine("\n You are logged out.\n");
                                                break;

                                            default:
                                                Console.WriteLine("\n Wrong input");
                                                break;

                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"\n {ex.Message}");
                                    }

                                }
                                while (selectedItem != 6);
                            }
                            break;

                        case 3:
                            Console.Write("\n Enter User Name: ");
                            user.email = Console.ReadLine();

                            message = userServices.ForgotPassword(forgot);

                            if (!string.IsNullOrEmpty(message))
                                Console.WriteLine("\n" + message);
                            break;

                        case 0:
                            break;

                        default:
                            Console.WriteLine("\n Wrong Input");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n Exception Occured: {ex.Message}");
                }
            }
            while (selectedItem != 0);

        }
    }
}
