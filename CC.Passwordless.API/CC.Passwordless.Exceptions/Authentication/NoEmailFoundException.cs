namespace CC.Passwordless.Exceptions.Authentication
{
    public class NoEmailFoundException: Exception
    {
        public NoEmailFoundException(string message) : base(message) { }
    }
}