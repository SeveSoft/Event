using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
using Event.Common.Models;
using Event.Web.Models;
using Event.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Event.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEventDataService _eventDataService;

        public AccountController(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        [AllowAnonymous]
        public async Task Login()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder().WithRedirectUri("https://localhost:44304").Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [AllowAnonymous]
        public async Task SignUpLogin()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder().WithRedirectUri("https://localhost:44304/SignUp").Build();
            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task<ActionResult> Profile()
        {
            var nameIdentifier = User.Claims.Single(t => t.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _eventDataService.GetUserAsync(new UserServiceModel { IdentityId = nameIdentifier });
            var model = new HomeModel { User = user };
            return View(model);
        }


        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().WithRedirectUri("https://localhost:44304").Build();

            // Logout from Auth0
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            // Logout from the application
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
