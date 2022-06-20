using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Event.Common.Enum;
using Event.Common.Models;
using Event.Web.Authorization;
using Event.Web.Models;
using Event.Web.Services;

namespace Event.Web.Controllers
{
    public class TalkController : Controller
    {
        private readonly IEventDataService _eventDataService;

        public TalkController(IEventDataService eventDataService)
        {
            _eventDataService = eventDataService;
        }

        public async Task<IActionResult> ReserveSeat(ReserveSeatModel model)
        {
            model.MyConventions = await _eventDataService.GetMyConventionsAsync(new UserServiceModel { Id = User.Claims.UserId() });

            if (ModelState.IsValid)
            {
                await _eventDataService.RegisterSeatAsync(new RegisterSeatServiceModel { TalkId = model.SelectedTalkId, UserId = User.Claims.UserId() });
                return RedirectToAction("MySeatReservations", "Talk");
            }

            return View(model);
        }

        public async Task<IActionResult> MySeatReservations()
        {
            var conventions = await _eventDataService.GetMyConventionsAsync(new UserServiceModel { Id = User.Claims.UserId() });
            var list = new List<Convention>();
            conventions.ForEach(c => c.Venues.ForEach(v => v.Talks.ForEach(t =>
            {
                if (t.HasReservedSeat && !list.Contains(c))
                {
                    list.Add(c);
                }
            })));
            var model = new MySeatReservationModel()
            {
                MySeatReservations = list
            };

            return View(model);
        }


        [AuthorizeRoles(UserRole.Speaker)]
        public async Task<IActionResult> RegisterTalk(RegisterTalkModel model)
        {
            model.MyConventions = await _eventDataService.GetMyConventionsAsync(new UserServiceModel { Id = User.Claims.UserId() });
            return View(model);
        }

        [AuthorizeRoles(UserRole.Speaker)]
        public async Task<IActionResult> AddTalk(RegisterTalkModel model)
        {
            if (model.SelectedConventionId > 0 && !ModelState.IsValid)
            {
                model = new RegisterTalkModel
                {
                    Topics = await _eventDataService.GetSpeakerTopicsAsync(),
                    MyConventions = await _eventDataService.GetMyConventionsAsync(new UserServiceModel { Id = User.Claims.UserId() }),
                    SelectedConventionId = model.SelectedConventionId
                };
                return View("RegisterTalk", model);
            }

            var topics = await _eventDataService.GetSpeakerTopicsAsync();

            var talkModel = new TalkServiceModel
            {
                ConventionId = model.SelectedConventionId,
                VenueId = model.SelectedVenueId,
                UserId = User.Claims.UserId(),
                Topic = topics.Single(t => t.Id == model.SelectedTopicId).Name,
                Info = model.Info,

            };
            await _eventDataService.RegisterTalkAsync(talkModel);
            return RedirectToAction("Index", "Home");
        }
    }
}
