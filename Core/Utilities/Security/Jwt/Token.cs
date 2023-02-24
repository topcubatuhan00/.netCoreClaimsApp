namespace Core.Utilities.Security.Jwt
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
