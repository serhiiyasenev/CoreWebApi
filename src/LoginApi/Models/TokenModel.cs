namespace LoginApi.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int ExpiresInSeconds { get; set; }
        public string TokenType { get; set; }
    }
}
