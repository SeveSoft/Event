using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Event.Api.Services;
using Event.Common.Models;
using Event.Data;
using Microsoft.AspNetCore.Authorization;

namespace Event.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ServiceController : Controller
    {
        private readonly IVenueService _venueService;
        private readonly ITopicService _topicService;
        private readonly IDataRepository _dataRepository;

        public ServiceController(IVenueService venueService, ITopicService topicService, IDataRepository dataRepository)
        {
            _venueService = venueService;
            _topicService = topicService;
            _dataRepository = dataRepository;
        }

        [HttpGet]
        [Route("GetVenues")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> GetVenues()
        {
            try
            {
                var venues = await _venueService.GetVenuesAsync(HttpContext.RequestAborted);
                return new JsonResult(venues);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("GetTopics")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> GetTopics()
        {
            try
            {
                var topics = await _topicService.GetTopicsAsync(HttpContext.RequestAborted);
                return new JsonResult(topics);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("GetConventions")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> GetConventions()
        {
            try
            {
                var conventions = _dataRepository.GetConventions(HttpContext.RequestAborted);
                return new JsonResult(conventions);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("GetMyConventions")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> GetMyConventions(UserServiceModel user)
        {
            try
            {
                var conventions = _dataRepository.GetMyConventions(user, HttpContext.RequestAborted);
                return new JsonResult(conventions);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpPut]
        [Route("AddVenueToConvention")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.UnprocessableEntity)]
        public async Task AddVenueToConvention([Required] AddVenueServiceModel model)
        {
            try
            {
                _dataRepository.AddVenueToConvention(model, HttpContext.RequestAborted);
            }
            catch (Exception e)
            {
                //do some Error log
            }
        }

        [HttpPut]
        [Route("CreateConvention")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> CreateConvention([Required] Convention convention)
        {
            try
            {
                var conventionId = _dataRepository.CreateConvention(convention.Name, HttpContext.RequestAborted);
                return new JsonResult(conventionId);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpPut]
        [Route("CreateTalk")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task CreateTalk([Required] TalkServiceModel model)
        {
            try
            {
                _dataRepository.CreateTalk(model, HttpContext.RequestAborted);
            }
            catch (Exception e)
            {
                //do some Error log
            }
        }

        [HttpPut]
        [Route("RegisterSeat")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task RegisterSeat([Required] RegisterSeatServiceModel model)
        {
            try
            {
                _dataRepository.RegisterSeat(model, HttpContext.RequestAborted);
            }
            catch (Exception e)
            {
                //do some Error log
            }
        }

        [HttpPut]
        [Route("RegisterUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> RegisterUser([Required] UserServiceModel model)
        {
            try
            {
                var user = _dataRepository.RegisterUser(model, HttpContext.RequestAborted);
                return new JsonResult(user);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("GetUser")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task<IActionResult> GetUser([Required] UserServiceModel model)
        {
            try
            {
                var user = _dataRepository.GetUser(model, HttpContext.RequestAborted);
                return new JsonResult(user);
            }
            catch (Exception e)
            {
                //do some Error log
                return new NotFoundResult();
            }
        }

        [HttpPut]
        [Route("SignUpForConvention")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        public async Task SignUpForConvention([Required] SignUpServiceModel model)
        {
            try
            {
                _dataRepository.SignUpForConvention(model, HttpContext.RequestAborted);
            }
            catch (Exception e)
            {
                //do some Error log
            }
        }
    }
}
