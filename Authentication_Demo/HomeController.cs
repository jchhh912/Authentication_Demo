using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Authentication_Demo
{
    [Authorize]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Content($"Hello {User.Identity?.Name}");
        }
        [HttpGet("signin")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn()
        {

            var redirectUrl = Url.Action(nameof(HomeController.Index), "Home");
            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUrl },
                OpenIdConnectDefaults.AuthenticationScheme);

        }
    }
}
