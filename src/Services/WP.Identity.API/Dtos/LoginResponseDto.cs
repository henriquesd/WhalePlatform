namespace WP.Identity.API.Dtos
{
    public class LoginResponseDto
    {
        public required string AccessToken { get; set; }
        public required double ExpiresIn { get; set; }
        public required UserTokenDto UserToken { get; set; }
    }

    public class UserTokenDto
    {
        public required string Id { get; set; }
        public required string Email { get; set; }
        public IEnumerable<UserClaimDto>? Claims { get; set; }
    }

    public class UserClaimDto
    {
        public required string Value { get; set; }
        public required string Type { get; set; }
    }
}
