using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using WP.Identity.API.Dtos;

namespace WP.Identity.API.Controllers.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class RegisterRequestExample : IExamplesProvider<RegisterDto>
    {
        public RegisterDto GetExamples()
        {
            return new RegisterDto
            {
                Email = GenerateUniqueEmail(),
                Password = "Test@123",
                PasswordConfirmation = "Test@123"
            };
        }

        private string GenerateUniqueEmail()
        {
            return $"user{Guid.NewGuid()}@example.com";
        }
    }
}
