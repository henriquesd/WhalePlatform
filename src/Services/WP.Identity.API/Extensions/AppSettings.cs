namespace WP.Identity.API.Extensions
{
    public class AppSettings
    {
        public required string Secret { get; set; }
        public int ExpirationHours { get; set; }
        public required string Issuer { get; set; }
        public required string ValidAudience { get; set; }
    }
}
