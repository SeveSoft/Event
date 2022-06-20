using Event.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Event.Common.Models;
using Event.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace Event.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventDataService _eventDataService;

        public HomeController(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
           var model = new HomeModel
            {
                UpcomingConventions = await _eventDataService.GetConventionsAsync(),
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> GoogleLoginCallback()
        //{
        //    var result = await HttpContext.AuthenticateAsync(ExternalAuthenticationDefaults.AuthenticationScheme);

        //    var externalClaims = result.Principal.Claims.ToList();

        //    var subjectIdClaim = externalClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        //    var subjectValue = subjectIdClaim.Value;

        //    var user = _eventDataService.GetByGoogleId(subjectValue);
        //    var claims = CreateClaims(user);

        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);

        //    await HttpContext.SignOutAsync(ExternalAuthenticationDefaults.AuthenticationScheme);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //    return LocalRedirect(result.Properties.Items["returnUrl"]);
        //}

        //public static class ExternalAuthenticationDefaults
        //{
        //    public const string AuthenticationScheme = "ExternalIdentity";
        //}
        //private List<Claim> CreateClaims(UserServiceModel user)
        //{
        //    return new List<Claim>
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //        new Claim(ClaimTypes.Name, user.Name),
        //        new Claim(ClaimTypes.Role, user.UserRole.ToString()),
        //        new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
        //        new Claim(ClaimTypes.Email, user.Email),
        //        new Claim(ClaimTypes.StreetAddress, user.Address),
        //    };

        //}
    }
}
