namespace CC.Passwordless.Exceptions.Authentication
{
    public class EmailNotificationException : Exception
    {
        public EmailNotificationException(string message) : base(message) { }
    }
}
