namespace CC.Passwordless.API.Models.Response
{
    public class AuthenticationResponse<T>
    {
        public T? Data { get; set; }
        public bool Error { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
