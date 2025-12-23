namespace WP.Web.Client.Models
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public double ExpiresIn { get; set; }
        public UserTokenDto? UserToken { get; set; }
    }

    public class UserTokenDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<UserClaimDto>? Claims { get; set; }
    }

    public class UserClaimDto
    {
        public string Value { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
