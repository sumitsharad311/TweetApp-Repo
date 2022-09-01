using System;

namespace TWEETApp.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException()
        {

        }
        public CustomException(string message) : base(String.Format($" Exception Occured: {message}"))
        {

        }
    }
}
