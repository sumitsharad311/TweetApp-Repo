using TWEETApp.Exceptions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TWEETApp.Services
{
    public class DataValidationServices
    {
        public bool DateValidation(string date)
        {
            try
            {
                DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EmailValidation(string email)
        {
            try
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                    return true;
                else
                    return false;
                throw new CustomException(email + " is not Valid Email Address");
            }
            catch
            {
                return false;
            }

        }
        public bool PasswordValidation(string password)
        {
            try
            {
                string matchPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
                bool isMatch = Regex.IsMatch(password, matchPattern);
                if (!isMatch)
                {
                    throw new Exception("\nPassword must contains \n1.At least one lower case letter," +
                    "\n2.At least one upper case letter," +
                    "\n3.At least one number," +
                    "\n4.At least 8 characters length");

                }
                return true;
            }
            catch (CustomException ex)
            {
                Console.WriteLine($"\nException Occured: {ex.Message}");
                return true;
            }
        }
        public bool ContactValidation(string contact)
        {
            try
            {
                string matchPattern = @"^([0]|\+91)?[6-9]\d{9}$";
                bool isMatch = Regex.IsMatch(contact, matchPattern);
                if (!isMatch)
                {
                    throw new Exception("\n Contact number must have 10 digits starting iwth +91 or 0 as country code");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
