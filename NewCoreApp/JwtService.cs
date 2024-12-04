namespace NewCoreApp
{
    public class JwtService : IJwtService   
    {
        private readonly string _secretKey;

        public JwtService(string secretKey)
        {
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        }

        public string GenerateToken(string userId)
        {
            // Token generation logic using _secretKey
            return "dummy-token";
        }

        public bool ValidateToken(string token)
        {
            // Token validation logic
            return token == "dummy-token";
        }
    }
}
