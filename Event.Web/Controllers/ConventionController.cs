using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Event.Common.Enum;
using Event.Web.Authorization;
using Event.Web.Models;
using Event.Web.Services;

namespace Event.Web.Controllers
{
    [AuthorizeRoles(UserRole.Admin)]
    public class ConventionController : Controller
    {
        private readonly IEventDataService _eventDataService;

        public ConventionController(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ConventionModel
            {
                Conventions = await _eventDataService.GetConventionsAsync()
            };
            return View("Index", model);
        }

        public async Task<IActionResult> ShowConvention(int conventionId)
        {
            var conventions = await _eventDataService.GetConventionsAsync();
            var convention = conventions.SingleOrDefault(t => t.Id == conventionId);

            ViewBag.Title = $"CONVENTION: {convention.Name}".ToUpper();
            var model = new ConventionModel { SelectedConvention = convention };
            return View("ShowConvention", model);

        }

        public async Task<IActionResult> ShowVenues(int conventionId)
        {
            var conventions = await _eventDataService.GetConventionsAsync();
            var model = new ConventionModel
            {
                Venues = await _eventDataService.GetVenuesAsync(),
                Conventions = conventions,
                SelectedConvention = conventions.SingleOrDefault(t => t.Id == conventionId)
            };
            return View("AddVenue", model);
        }

        public async Task<IActionResult> CreateConvention(ConventionModel model)
        {
            if (model.SelectedConvention != null)
            {
                await _eventDataService.CreateConventionAsync(model.SelectedConvention);
                return await Index();
            }
            return View(model);
        }

        public async Task<IActionResult> AddVenue(string venueId, int conventionId)
        {
            var venues = await _eventDataService.GetVenuesAsync();
            await _eventDataService.AddVenueToConventionAsync(venues.Single(t => t.VenueId == venueId), conventionId);
            return await ShowConvention(conventionId);
        }
    }
}
