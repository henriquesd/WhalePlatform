using Microsoft.AspNetCore.Mvc;
using WP.Web.Models;

namespace WP.Web.Interfaces
{
    public interface IAuthService
    {
        Task<ActionResult> Register(UserRegister userRegister);
        Task<ActionResult> Login(UserLogin userLogin);
    }
}