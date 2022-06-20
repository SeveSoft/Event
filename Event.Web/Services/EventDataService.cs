using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Event.Common.Models;
using Event.Common.Utility;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Event.Web.Services
{
    public class EventDataService :IEventDataService
    {
        private readonly string _baseUrl;
        private readonly RestClient _restClient;
        private readonly string _apiKey;

        public EventDataService(IConfiguration configuration)
        {
            _restClient = new RestClient();
            _apiKey = Encryptor.DecryptString(configuration["ApiKey"]);
            _baseUrl = configuration["ApiBaseUrl"];
        }

        public async Task<List<Topic>> GetSpeakerTopicsAsync()
        {
            var url = $"{_baseUrl}GetTopics";
            return await _restClient.MakeTheCall<List<Topic>>(new Uri(url), HttpMethod.Get, _apiKey);
        }

        public async Task<List<Venue>> GetVenuesAsync()
        {
            var url = $"{_baseUrl}GetVenues";
            return await _restClient.MakeTheCall<List<Venue>>(new Uri(url), HttpMethod.Get, _apiKey);
        }

        public async Task<List<Convention>> GetConventionsAsync()
        {
            var url = $"{_baseUrl}GetConventions";
            return await _restClient.MakeTheCall<List<Convention>>(new Uri(url), HttpMethod.Get, _apiKey);
        }

        public async Task AddVenueToConventionAsync(Venue venue, int conventionId)
        {
            var model = new AddVenueServiceModel
            {
                ConventionId = conventionId,
                Venue = venue,
            };
            
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}AddVenueToConvention";
            await _restClient.MakeTheCall<int>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task<int> CreateConventionAsync(Convention convention)
        {
            var jsonBody = JsonConvert.SerializeObject(convention);
            var url = $"{_baseUrl}CreateConvention";
            return  await _restClient.MakeTheCall<int>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task<UserServiceModel> RegisterUserAsync(UserServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}RegisterUser";
            return await _restClient.MakeTheCall<UserServiceModel>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task<UserServiceModel> GetUserAsync(UserServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}GetUser";
            return await _restClient.MakeTheCall<UserServiceModel>(new Uri(url), HttpMethod.Get, _apiKey, jsonBody);
        }

        public async Task SignUpForConventionAsync(SignUpServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}SignUpForConvention";
            await _restClient.MakeTheCall<int>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task RegisterTalkAsync(TalkServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}CreateTalk";
            await _restClient.MakeTheCall<int>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task RegisterSeatAsync(RegisterSeatServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}RegisterSeat";
            await _restClient.MakeTheCall<int>(new Uri(url), HttpMethod.Put, _apiKey, jsonBody);
        }

        public async Task<List<Convention>> GetMyConventionsAsync(UserServiceModel model)
        {
            var jsonBody = JsonConvert.SerializeObject(model);
            var url = $"{_baseUrl}GetMyConventions";
            return await _restClient.MakeTheCall<List<Convention>>(new Uri(url), HttpMethod.Get, _apiKey, jsonBody);
        }
    }
}
