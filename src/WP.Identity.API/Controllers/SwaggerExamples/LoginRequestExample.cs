using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using WP.Identity.API.Dtos;

namespace WP.Identity.API.Controllers.SwaggerExamples
{
    [ExcludeFromCodeCoverage]
    public class LoginRequestExample : IExamplesProvider<LoginDto>
    {
        public LoginDto GetExamples()
        {
            return new LoginDto
            {
                Email = "user@example.com",
                Password = "Test@123"
            };
        }
    }
}
