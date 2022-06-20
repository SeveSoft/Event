using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Event.Common.Models;
using Event.Web.Models;
using Event.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IEventDataService _eventDataService;

        public SignUpController(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new SignUpModel
            {
                Conventions = await _eventDataService.GetConventionsAsync(),
            };
            return View(model);
        }

        public async Task<IActionResult> SignUpUserForConvention(SignUpModel model)
        {
            if (model.SelectedConventionId > 0 && !ModelState.IsValid)
            {
                model = new SignUpModel
                {
                    Conventions = await _eventDataService.GetConventionsAsync(),
                    SelectedConventionId = model.SelectedConventionId
                };
                return View("Index", model);
            }

            var user = new UserServiceModel
            {
                IdentityId = User.Claims.Single(t => t.Type == ClaimTypes.NameIdentifier).Value,
                IdentityName = User.Claims.FirstOrDefault(t => t.Type == "name")?.Value,
                UserRole = model.SelectedUserRole,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };
            user = await _eventDataService.RegisterUserAsync(user);
            var signUpModel = new SignUpServiceModel
            {
                ConventionId = model.SelectedConventionId,
                UserId = user.Id,
                VenueId = model.SelectedVenueId
            };
            await _eventDataService.SignUpForConventionAsync(signUpModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
